const SlackWebhook = require('slack-webhook');
// send message to slack
function SendMessageSlack(msg, url) {
  const slack = new SlackWebhook(url, {
    defaults: {
      username: 'StockWatchBot',
      channel: '#Money',
      icon_emoji: ':robot_face:',
    },
  });

  slack.send(msg);
}
module.exports = SendMessageSlack;
