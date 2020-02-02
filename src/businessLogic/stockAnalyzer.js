// import npm/node packages

const fs = require('fs');
const https = require('https');
const SlackWebhook = require('slack-webhook');
const path = require('path');
const winston = require('winston');
require('winston-daily-rotate-file');

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

  sendMessageTest(msg) { // eslint-disable-line no-unused-vars
    this.logger.info(msg); // eslint-disable-line no-console
  }

  sendMessageSlack(msg) {
    this.logger.info(msg);
    this.logger.info('Pushing message to Slack');
    const slack = new SlackWebhook(Secrets.slackHookUrl, {
      defaults: {
        username: 'StockWatchBot',
        channel: '#Money',
        icon_emoji: ':robot_face:',
      },
    });

    slack.send(msg);
  }

  updateAlertHistory(ticker) { // eslint-disable-line no-unused-vars
    this.alertHistory[ticker] = { alertTime: Date.now() };
    this.logger.info(`Ticker: ${ticker} updated in json History file with time: ${this.alertHistory[ticker].alertTime}`);
  }

  saveAlertHistory() {
    fs.writeFileSync(
      path.resolve(__dirname, this.alertHistoryPath),
      JSON.stringify(this.alertHistory)
    );
  }

  cycleThroughStocks(stockJson) {
    this.logger.info('Cycle through Stocks, where thresholds are met');
    stockJson.finance.result[0].quotes.forEach((qoute) => {
      if (qoute.marketCap.raw > this.configuration.minNotifyMarketCap
        && qoute.averageDailyVolume3Month.raw > this.configuration.minAvgDailyVolume
        && qoute.regularMarketChangePercent.raw > this.configuration.minPercentIncrease
        && this.checkAlertHistory(qoute.symbol)) {
        this.logger.info(`Stock: ${qoute.symbol} matches within boundry conditions`);
        this.updateAlertHistory(qoute.symbol);
        this.sendMessageTest(`Stock:  ${qoute.longName} : https://finance.yahoo.com/quote/${qoute.symbol}/
        Percent Change: ${qoute.regularMarketChangePercent.fmt}
        Market Cap: ${qoute.marketCap.fmt}`);
      }
    });
    this.saveAlertHistory();
  }

  startService() {
    this.logger.info('Application Start');


    try {
      // log config values

      this.logger.info('Config Values');
      this.logger.info(`Percent Increase Threshold:${this.configuration.minPercentIncrease.toString()}`);
      this.logger.info(`Min Market Cap Threshold:${this.configuration.minNotifyMarketCap.toString()}`);
      this.logger.info(`Alert Cooldown Increase in Days:${(this.stockAlertCooldownInMs / (1000 * 60 * 60 * 24)).toString()}`);


      this.logger.info('Pull Top Performing Stocks From Api');
      // Pull Stocks info from API
      https.get(this.configuration.apiTopGainerUrl, (response) => {
        let body = '';
        response.on('data', (chunk) => {
          body += chunk;
        });
        response.on('end', () => {
          const stockInfo = JSON.parse(body);
          this.cycleThroughStocks(stockInfo);
        });
      });
      // sendMessageSlack('Wiljum Test Message');
    } catch (err) {
      this.logger.error(`Unexpected Error: ${err}`);
    }
  }
}
module.exports = StockAnalyzer;
