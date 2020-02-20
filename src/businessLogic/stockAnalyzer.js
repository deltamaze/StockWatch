// import npm/node packages

const fs = require('fs');
// const SlackWebhook = require('slack-webhook');
const path = require('path');
const winston = require('winston');
require('winston-daily-rotate-file');
const StocksApi = require('../services/yahooService');
const SendMessageSlack = require('../services/slackService');
// get api keys n stuff
const Secrets = require('../../secrets/secrets');// This resource is not in Source Control, replace Secrets.X with your own info

// set up logger
const transport = new winston.transports.DailyRotateFile({
  filename: path.resolve(__dirname, './StockWatchLog-%DATE%.txt'),
  datePattern: 'YYYY-MM-DD',
  maxFiles: '15',
});

const myFormat = winston.format.printf(info => `${info.timestamp} ${info.level}: ${info.message}`);

class StockAnalyzer {
  constructor() {
    // set up logger
    this.logger = winston.createLogger({
      format: winston.format.combine(
        winston.format.timestamp(),
        myFormat,
      ),
      transports: [
        new winston.transports.Console(),
        transport,
      ],
    });
    this.configuration = JSON.parse(fs.readFileSync(path.resolve(__dirname, '../../appConfig.json')));
    this.alertHistoryPath = '../../alertHistory.json';
    this.alertHistory = JSON.parse(
      fs.readFileSync(path.resolve(__dirname, this.alertHistoryPath)),
    );
    this.stockAlertCooldownInMs = (
      1000 * 60 * 60 * 24 * this.configuration.stockAlertCooldownInDays);
  }

  checkAlertHistory(ticker) { // eslint-disable-line no-unused-vars
    this.logger.info(`Checking ${ticker} ticker against History`);
    if (this.alertHistory[ticker] == null) {
      this.logger.info('No History Found');
      return true;
    }
    if (this.alertHistory[ticker].alertTime < (Date.now() - this.stockAlertCooldownInMs)) {
      this.logger.info('Last history before treshold');
      return true;
    }
    this.logger.info('Recent History Found');
    return false;
  }

  updateAlertHistory(ticker) { // eslint-disable-line no-unused-vars
    this.alertHistory[ticker] = { alertTime: Date.now() };
    this.logger.info(`Ticker: ${ticker} updated in json History file with time: ${this.alertHistory[ticker].alertTime}`);
  }

  cycleThroughStocks(stockJson) {
    this.logger.info('Cycle through Stocks, where thresholds are met');
    stockJson.finance.result[0].quotes.forEach((qoute) => {
      if (
        (typeof qoute.marketCap != 'undefined')
        && (typeof qoute.averageDailyVolume3Month != 'undefined')
        && (typeof qoute.regularMarketChangePercent != 'undefined')
        && qoute.marketCap.raw > this.configuration.minNotifyMarketCap
        && qoute.averageDailyVolume3Month.raw > this.configuration.minAvgDailyVolume
        && Math.abs(qoute.regularMarketChangePercent.raw) > this.configuration.minPercentChange
        && this.checkAlertHistory(qoute.symbol)) {
        this.logger.info(`Stock: ${qoute.symbol} matches within boundry conditions`);
        this.updateAlertHistory(qoute.symbol); //  refactorTD: pull out and test
        const msg = `Stock:  ${qoute.longName} : https://finance.yahoo.com/quote/${qoute.symbol}/
        Percent Change: ${qoute.regularMarketChangePercent.fmt}
        Market Cap: ${qoute.marketCap.fmt}`;
        this.logger.info(msg);
        this.logger.info('Pushing message to Slack');
        SendMessageSlack(msg, Secrets.slackHookUrl);
      }
    });
  }

  startService() {
    this.logger.info('Application Start');


    this.logger.info('Config Values');
    this.logger.info(`Percent Change Threshold:${this.configuration.minPercentChange.toString()}`);
    this.logger.info(`Min Market Cap Threshold:${this.configuration.minNotifyMarketCap.toString()}`);
    this.logger.info(`Alert Cooldown in Days:${(this.stockAlertCooldownInMs / (1000 * 60 * 60 * 24)).toString()}`);


    this.logger.info('Pull Top Performing Stocks From Api');

    Promise.all([
      StocksApi.getStocks(this.configuration.apiTopGainerUrl),
      StocksApi.getStocks(this.configuration.apiTopLoserUrl)
    ]).then((data) => {
      // expected returned json objects for both web calls.
      // find the child element/array we need and merge them together
      this.cycleThroughStocks(data[0]);// gains
      this.cycleThroughStocks(data[1]);// loss
      fs.writeFileSync(
        path.resolve(__dirname, this.alertHistoryPath),
        JSON.stringify(this.alertHistory)
      );
    }).catch(
      (err) => {
        this.logger.error((`Unexpected Error: ${err}`));
      }
    );
  }
}
module.exports = StockAnalyzer;
