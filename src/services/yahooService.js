
const https = require('https');

// call yahoo api to get top gainer and losers
class StocksApi {
  static getStocks() {
    const returnPromise = new Promise((resolve, reject) => {
      // We call resolve(...) when what we were doing asynchronously was successful,
      // and reject(...) when it failed.
      // In this example, we use setTimeout(...) to simulate async code.
      // In reality, you will probably be using something like XHR or an HTML5 API.

      setTimeout(() => {
        try {
          resolve('Success!');
        } catch (err) {
          reject(err);
        } // Yay! Everything went well!
      }, 250);
    });
    return returnPromise;
  }
}
module.exports = StocksApi;
