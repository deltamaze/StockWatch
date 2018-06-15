// Import Libs
const nodemailer = require('nodemailer');
const Secrets = require('./secrets/secrets');
const fs = require('fs');
const https = require('https');

// Configs
const minPercentIncrease = 10;
const minNotifyMarketCap = 3000000000;
const stockAlertCooldownInMs = (1000 * 60 * 60 * 24 * 7);// 7 days;
const apiUrl = 'https://query1.finance.yahoo.com/v1/finance/screener/predefined/saved?formatted=true&lang=en-US&region=US&scrIds=day_gainers&start=0&count=3';
const alertHistory = JSON.parse(fs.readFileSync('alertHistory.json'));

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
  transporter.sendMail(mailOptions, (error, info) => {
    if (error) {
      console.log(error); // eslint-disable-line no-console
    }
    console.log('Message sent: %s', info.messageId); // eslint-disable-line no-console
  });
};
const sendEmailTest = (msg) => { // eslint-disable-line no-unused-vars
  console.log(msg); // eslint-disable-line no-console
};
//End Email Config

//Business Logic Functions

const checkAlertHistory = (ticker) => { // eslint-disable-line no-unused-vars
  if (alertHistory[ticker] != null &&
    alertHistory[ticker].alertTime < Date.now() - stockAlertCooldownInMs) {
    return false;
  }
  return true;
};

const updateAlertHistory = (ticker) => { // eslint-disable-line no-unused-vars
  alertHistory[ticker] = { alertTime: Date.now() };
};

const saveAlertHistory = () => {
  fs.writeFileSync('alertHistory.json', JSON.stringify(alertHistory));
};

const CycleThroughStocks = (stockJson) => {
  stockJson.finance.result[0].quotes.forEach((qoute) => {
    if (qoute.marketCap.raw > minNotifyMarketCap &&
      qoute.twoHundredDayAverageChange.raw > minPercentIncrease &&
      checkAlertHistory(qoute.symbol)) {
      updateAlertHistory(qoute.symbol);
      sendEmailTest(`Stock: ${qoute.symbol} 
        2 Day Percent Change: ${qoute.twoHundredDayAverageChange.fmt}
        2 Day Percent Change: ${qoute.marketCap.fmt}`);
    }
  });
  saveAlertHistory();
};

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

