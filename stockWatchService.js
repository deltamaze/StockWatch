// Import Libs
const fs = require('fs');
const https = require('https');
const SlackWebhook = require('slack-webhook');
const path = require('path');
const winston = require('winston');
require('winston-daily-rotate-file');
const Secrets = require('./secrets/secrets');// This resource is not in Source Control, replace Secrets.X with your own info


class StockWatch {
  constructor(minPercentIncreaseAmt, minAvgDailyVolumeAmt, minNotifyMarketCapAmt) {
    const transport = new winston.transports.DailyRotateFile({
      filename: path.resolve(__dirname, './StockWatchLog-%DATE%.txt'),
      datePattern: 'YYYY-MM-DD',
      maxFiles: '15d',
    });

    const myFormat = winston.format.printf(info => `${info.timestamp} ${info.level}: ${info.message}`);

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


    this.logger.info('StockWatch Service Initialized');

    this.minPercentIncrease = minPercentIncreaseAmt;
    this.minAvgDailyVolume = minAvgDailyVolumeAmt;
    this.minNotifyMarketCap = minNotifyMarketCapAmt;
    this.stockAlertCooldownInMs = (1000 * 60 * 60 * 24 * 7);// 7 days;

    this.apiUrl = 'https://query1.finance.yahoo.com/v1/finance/screener/predefined/saved?formatted=true&lang=en-US&region=US&scrIds=day_gainers&start=0&count=3';

    this.logger.info(`Percent Increase Threshold:${this.minPercentIncrease.toString()}`);
    this.logger.info(`Min Market Cap Threshold:${this.minNotifyMarketCap.toString()}`);
    this.logger.info(`Alert Cooldown Increase in Days:${(this.stockAlertCooldownInMs / (1000 * 60 * 60 * 24)).toString()}`);

    this.alertHistory = JSON.parse(fs.readFileSync(path.resolve(__dirname, './alertHistory.json')));
  }

  sendMessageSlack(msg) { // eslint-disable-line no-unused-vars
    this.slack = new SlackWebhook(Secrets.slackHookUrl, {
      defaults: {
        username: 'StockWatchBot',
        channel: '#Money',
        icon_emoji: ':robot_face:',
      },
    });
    this.slack.send(msg);
  }

  checkAlertHistory(ticker) { // eslint-disable-line no-unused-vars
    if (this.alertHistory[ticker] == null) {
      return true;
    }
    if (this.alertHistory[ticker].alertTime < (Date.now() - this.stockAlertCooldownInMs)) {
      return true;
    }
    return false;
  }

  runService() {
    try {
      this.logger.info('Parse Alert History Json');
      
      // Business Logic Functions

      const updateAlertHistory = (ticker) => { // eslint-disable-line no-unused-vars
        alertHistory[ticker] = { alertTime: Date.now() };
        this.logger.info(`Ticker: ${ticker} updated in json History file with time: ${alertHistory[ticker].alertTime}`);
      };

      const saveAlertHistory = () => {
        fs.writeFileSync(path.resolve(__dirname, './alertHistory.json'), JSON.stringify(alertHistory));
      };

      const CycleThroughStocks = (stockJson) => {
        this.logger.info('Cycle through Stocks, where thresholds are met');
        stockJson.finance.result[0].quotes.forEach((qoute) => {
          if (qoute.marketCap.raw > this.minNotifyMarketCap
            && qoute.averageDailyVolume3Month.raw > this.minAvgDailyVolume
            && qoute.regularMarketChangePercent.raw > this.minPercentIncrease
            && checkAlertHistory(qoute.symbol)) {
            this.logger.info(`Stock: ${qoute.symbol} matches within boundry conditions`);
            updateAlertHistory(qoute.symbol);
            this.sendMessageSlack(`Stock:  ${qoute.longName} : https://finance.yahoo.com/quote/${qoute.symbol}/
          Percent Change: ${qoute.regularMarketChangePercent.fmt}
          Market Cap: ${qoute.marketCap.fmt}`);
          }
        });
        saveAlertHistory();
      };
      logger.info('Pull Top Performing Stocks From Api');
      // Pull Stocks info from API
      https.get(apiUrl, (response) => {
        let body = '';
        response.on('data', (chunk) => {
          body += chunk;
        });
        response.on('end', () => {
          const stockInfo = JSON.parse(body);
          CycleThroughStocks(stockInfo);
        });
      });
    } catch (err) {
      logger.error(`Unexpected Error: ${err}`);
    }
  }
}

// unit test
// mock api data
