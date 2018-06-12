const nodemailer = require('nodemailer');
const Secrets = require('./secrets/secrets');
const fs = require('fs');

// Stock Alert Parameters
// percent increase over 2 day span
const percentIncreaseThreshold = 60; // eslint-disable-line no-unused-vars

// only 1 email per stock per set day below
const stockAlertCooldown = 7; // eslint-disable-line no-unused-vars
let alertHistory = JSON.parse(fs.readFileSync('alertHistory.json'));

const transporter = nodemailer.createTransport({ // eslint-disable-line no-unused-vars
  service: 'gmail',
  auth: {
    user: Secrets.gmailUsername,
    pass: Secrets.gmailPassword,
  },
});
const checkAlertHistory = (ticker) => {
  if (alertHistory[ticker] != null) {
    return false;
  }
  return true;
};

const updateAlertHistory = (ticker) => {
  alertHistory[ticker] = { alertTime: Date.now() };
};

updateAlertHistory('testInsert');
console.log(alertHistory);
const mailOptions = { // eslint-disable-line no-unused-vars
  from: Secrets.gmailUsername, // sender address
  to: Secrets.notifyTargetEmail, // list of receivers
  subject: 'Stock Watch Alert!', // Subject line
  html: '', // plain text body
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

// download stock info
// loop through stock info and see if anything matches target parameters
// send email
