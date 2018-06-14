const nodemailer = require('nodemailer');
const Secrets = require('./secrets/secrets');
const fs = require('fs');

// const apiUrl = 'https://query1.finance.yahoo.com/v1/finance/screener/predefined/saved?formatted=true&lang=en-US&region=US&scrIds=day_gainers&start=0&count=5';
// Stock Alert Parameters
// percent increase over 2 day span
const percentIncreaseThreshold = 60; // eslint-disable-line no-unused-vars
// Only notify if market cap is greater than param below
const minNotifyMarketCap = 3000000000; // eslint-disable-line no-unused-vars
// only 1 email per stock per set day below
const stockAlertCooldownInMs = (1000 * 60 * 60 * 24 * 7);

const alertHistory = JSON.parse(fs.readFileSync('alertHistory.json'));
const testJsonPayload = JSON.parse(fs.readFileSync('testPayload.json'));


testJsonPayload.finance.result[0].quotes.forEach((qoute) => {
  console.log(qoute);
});
const transporter = nodemailer.createTransport({ // eslint-disable-line no-unused-vars
  service: 'gmail',
  auth: {
    user: Secrets.gmailUsername,
    pass: Secrets.gmailPassword,
  },
});
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

const mailOptions = { // eslint-disable-line no-unused-vars
  from: Secrets.gmailUsername, // sender address
  to: Secrets.notifyTargetEmail, // list of receivers
  subject: 'Stock Watch Alert!', // Subject line
  html: '', // plain text body
};
const saveAlertHistory = () => {
  fs.writeFileSync('alertHistory.json', JSON.stringify(alertHistory));
};

// const sendEmail = (msg) => { // Prod
//   mailOptions.html = msg;
//   transporter.sendMail(mailOptions, (error, info) => {
//     if (error) {
//       console.log(error); // eslint-disable-line no-console
//     }
//     console.log('Message sent: %s', info.messageId); // eslint-disable-line no-console
//   });
// };

const sendEmail = (msg) => { // Test
  console.log(msg); // eslint-disable-line no-console
};

sendEmail('Test Alert, Invest in Amazon ;)');
saveAlertHistory();
// download stock info
// loop through stock info and see if anything matches target parameters
// send email
