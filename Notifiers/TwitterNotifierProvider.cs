using System;
using System.Text;
using System.Collections.Generic;
using System.Text.Json;
using System.Net;
using System.Net.Http;
using StockWatch.Assets;
using StockWatch.Configuration;
using StockWatch.Data;
using StockWatch.Logging;

namespace StockWatch.Notifiers
{
    public class TwitterNotifierProvider : INotifierProvider
    {
        private SecretsDataModel secretsInfo;
        private const string postTweetUrl = "https://api.twitter.com/2/tweets";
        private const string requestTokenUrl = "https://api.twitter.com/oauth/request_token";
        public TwitterNotifierProvider(SecretsDataModel secrets)
        {
            this.secretsInfo = secrets;
        }
        public void Notify(List<AssetModel> assets)
        {
            // compose message
            string composedMessage = "";
            bool skipInitialLinebreak = true;
            foreach (AssetModel asset in assets)
            {
                if (skipInitialLinebreak)
                {
                    skipInitialLinebreak = false;
                }
                else
                {
                    composedMessage += "\n\n";
                }
                composedMessage +=
                (
                    $"Notify Stub for Asset {asset.Symbol}\n" +
                    $"Change Percent: {asset.PercentChange} etc.."
                );
            }



            // post
        }
        public async void GetToken()
        {
            var httpClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://api.twitter.com/oauth2/token");

            string url = "https://api.twitter.com/oauth2/token?oauth_consumer_key=" + secretsInfo.TwitterConnData.ApiKey + "&oauth_consumer_secret=" + secretsInfo.TwitterConnData.ApiSecret;

            var customerInfo = Convert.ToBase64String(
                new UTF8Encoding()
                                .GetBytes(secretsInfo.TwitterConnData.ApiKey + ":" + secretsInfo.TwitterConnData.ApiSecret));

            request.Headers.Add("Authorization", "Basic " + customerInfo);
            request.Content = new StringContent("grant_type=client_credentials", Encoding.UTF8,
                                                                "application/x-www-form-urlencoded");

            HttpResponseMessage response = await httpClient.SendAsync(request);

            string json = await response.Content.ReadAsStringAsync();

            dynamic item = JsonSerializer.Deserialize<object>(json);
        }
    }

}


//python example

// consumer_key = os.environ.get("CONSUMER_KEY")
// consumer_secret = os.environ.get("CONSUMER_SECRET")

// # Be sure to add replace the text of the with the text you wish to Tweet. You can also add parameters to post polls, quote Tweets, Tweet with reply settings, and Tweet to Super Followers in addition to other features.
// payload = {"text": "Hello world!"}

// # Get request token
// request_token_url = "https://api.twitter.com/oauth/request_token"
// oauth = OAuth1Session(consumer_key, client_secret=consumer_secret)

// try:
//     fetch_response = oauth.fetch_request_token(request_token_url)
// except ValueError:
//     print(
//         "There may have been an issue with the consumer_key or consumer_secret you entered."
//     )

// resource_owner_key = fetch_response.get("oauth_token")
// resource_owner_secret = fetch_response.get("oauth_token_secret")
// print("Got OAuth token: %s" % resource_owner_key)

// # Get authorization
// base_authorization_url = "https://api.twitter.com/oauth/authorize"
// authorization_url = oauth.authorization_url(base_authorization_url)
// print("Please go here and authorize: %s" % authorization_url)
// verifier = input("Paste the PIN here: ")

// # Get the access token
// access_token_url = "https://api.twitter.com/oauth/access_token"
// oauth = OAuth1Session(
//     consumer_key,
//     client_secret=consumer_secret,
//     resource_owner_key=resource_owner_key,
//     resource_owner_secret=resource_owner_secret,
//     verifier=verifier,
// )
// oauth_tokens = oauth.fetch_access_token(access_token_url)

// access_token = oauth_tokens["oauth_token"]
// access_token_secret = oauth_tokens["oauth_token_secret"]

// # Make the request
// oauth = OAuth1Session(
//     consumer_key,
//     client_secret=consumer_secret,
//     resource_owner_key=access_token,
//     resource_owner_secret=access_token_secret,
// )

// # Making the request
// response = oauth.post(
//     "https://api.twitter.com/2/tweets",
//     json=payload,
// )

// if response.status_code != 201:
//     raise Exception(
//         "Request returned an error: {} {}".format(response.status_code, response.text)
//     )

// print("Response code: {}".format(response.status_code))

// # Saving the response as JSON
// json_response = response.json()
// print(json.dumps(json_response, indent=4, sort_keys=True))