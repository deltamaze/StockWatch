const nodemailer = require('nodemailer');
const Secrets = require('../../secrets/secrets');// This resource is not in Source Control, replace Secrets.X with your own info

class EmailService {
  constructor() {
    this.transporter = nodemailer.createTransport({ // eslint-disable-line no-unused-vars
      service: 'gmail',
      auth: {
        user: Secrets.gmailUsername,
        pass: Secrets.gmailPassword,
      },
    });
    this.mailOptions = { // eslint-disable-line no-unused-vars
      from: Secrets.gmailUsername, // sender address
      to: Secrets.notifyTargetEmail, // list of receivers
      subject: 'Stock Watch Alert!', // Subject line
      html: '', // plain text body
    };
  }

  sendEmailProd(msg, callback) { // eslint-disable-line no-unused-vars
    this.mailOptions.html = msg;
    this.transporter.sendMail(this.mailOptions, (error) => {
      if (error) {
        // logger.info(error);
        callback(error);
      } else {
        // logger.info(`Email Sent: ${msg}`);
        callback(error);
      }
    });
  }
}
module.exports = EmailService;
