const nodemailer = require('nodemailer');
const Secrets = require('./secrets/secrets');

const transporter = nodemailer.createTransport({
  service: 'gmail',
  auth: {
    user: Secrets.gmailUsername,
    pass: Secrets.gmailPassword,
  },
});

const mailOptions = {
  from: Secrets.gmailUsername, // sender address
  to: 'to@email.com', // list of receivers
  subject: 'Subject of your email', // Subject line
  html: '<p>Your html here</p>'// plain text body
};

const sendEmail = (msg) => { }

