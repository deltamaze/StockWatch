// import npm/node packages
const nodemailer = require('nodemailer');
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

const logger = winston.createLogger({
  format: winston.format.combine(
    winston.format.timestamp(),
    myFormat,
  ),
  transports: [
    new winston.transports.Console(),
    transport,
  ],
});


class StockAnalyzer {
  constructor() {
    // set up logger
    this.configuration = JSON.parse(fs.readFileSync(path.resolve(__dirname, '../../appConfig.json')));
    this.stockAlertCooldownInMs = (
      1000 * 60 * 60 * 24 * this.configuration.stockAlertCooldownInDays);
  }

  startService() {
    logger.info('Application Start');


    try {
      // log config values

      logger.info('Config Values');
      logger.info(`Percent Increase Threshold:${this.configuration.minPercentIncrease.toString()}`);
      logger.info(`Min Market Cap Threshold:${this.configuration.minNotifyMarketCap.toString()}`);
      logger.info(`Alert Cooldown Increase in Days:${(this.stockAlertCooldownInMs / (1000 * 60 * 60 * 24)).toString()}`);

      logger.info('Parse Alert History Json');
      const alertHistory = JSON.parse(fs.readFileSync(path.resolve(__dirname, '../../alertHistory.json')));

      logger.info('Set up Main Transport');
      // Setup Email Services
      const transporter = nodemailer.createTransport({ // eslint-disable-line no-unused-vars
        service: 'gmail',
        auth: {
          user: Secrets.gmailUsername,
          pass: Secrets.gmailPassword,
        },
      });
      const mailOptions = { // eslint-disable-line no-unused-vars
        from: Secrets.gmailUsername, // sender address
        to: Secrets.notifyTargetEmail, // list of receivers
        subject: 'Stock Watch Alert!', // Subject line
        html: '', // plain text body
      };
      const sendEmailProd = (msg) => { // eslint-disable-line no-unused-vars
        mailOptions.html = msg;
        transporter.sendMail(mailOptions, (error) => {
          if (error) {
            logger.info(error);
          } else {
            logger.info(`Email Sent: ${msg}`);
          }
        });
      };
      const sendMessageTest = (msg) => { // eslint-disable-line no-unused-vars
        logger.info(msg); // eslint-disable-line no-console
      };
      const sendMessageSlack = (msg) => {
        const slack = new SlackWebhook(Secrets.slackHookUrl, {
          defaults: {
            username: 'StockWatchBot',
            channel: '#Money',
            icon_emoji: ':robot_face:',
          },
        });

        slack.send(msg);
      };

      // Business Logic Functions

      const checkAlertHistory = (ticker) => { // eslint-disable-line no-unused-vars
        if (alertHistory[ticker] == null) {
          return true;
        }
        if (alertHistory[ticker].alertTime < (Date.now() - this.stockAlertCooldownInMs)) {
          return true;
        }
        return false;
      };

      const updateAlertHistory = (ticker) => { // eslint-disable-line no-unused-vars
        alertHistory[ticker] = { alertTime: Date.now() };
        logger.info(`Ticker: ${ticker} updated in json History file with time: ${alertHistory[ticker].alertTime}`);
      };

      const saveAlertHistory = () => {
        fs.writeFileSync(path.resolve(__dirname, './alertHistory.json'), JSON.stringify(alertHistory));
      };

      const CycleThroughStocks = (stockJson) => {
        logger.info('Cycle through Stocks, where thresholds are met');
        stockJson.finance.result[0].quotes.forEach((qoute) => {
          if (qoute.marketCap.raw > this.minNotifyMarketCap
            && qoute.averageDailyVolume3Month.raw > this.configuration.minAvgDailyVolume
            && qoute.regularMarketChangePercent.raw > this.configuration.minPercentIncrease
            && checkAlertHistory(qoute.symbol)) {
            logger.info(`Stock: ${qoute.symbol} matches within boundry conditions`);
            updateAlertHistory(qoute.symbol);
            sendMessageTest(`Stock:  ${qoute.longName} : https://finance.yahoo.com/quote/${qoute.symbol}/
            Percent Change: ${qoute.regularMarketChangePercent.fmt}
            Market Cap: ${qoute.marketCap.fmt}`);
          }
        });
        saveAlertHistory();
      };
      logger.info('Pull Top Performing Stocks From Api');
      // Pull Stocks info from API
      https.get(this.configuration.apiTopGainerUrl, (response) => {
        let body = '';
        response.on('data', (chunk) => {
          body += chunk;
        });
        response.on('end', () => {
          const stockInfo = JSON.parse(body);
          CycleThroughStocks(stockInfo);
        });
      });
      // sendMessageSlack('Wiljum Test Message');
    } catch (err) {
      logger.error(`Unexpected Error: ${err}`);
    }
  }
}
module.exports = StockAnalyzer;
