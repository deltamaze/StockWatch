var nodemailer = require('nodemailer');
var Secrets = require("./secrets/secrets");
var mySecrets = new Secrets()

console.log(mySecrets.gmailPassword);
mySecrets.test();