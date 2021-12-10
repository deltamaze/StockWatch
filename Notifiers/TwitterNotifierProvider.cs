using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Web;

using StockWatch.Assets;
using StockWatch.Configuration;

namespace StockWatch.Notifiers
{
    public class TwitterNotifierProvider : INotifierProvider
    {
        private SecretsDataModel secretsInfo;
        private const string postTweetUrl = "https://api.twitter.com/2/tweets";
        private const string requestTokenUrl = "https://api.twitter.com/oauth/request_token";
        private AccessToken token = new AccessToken();
        public TwitterNotifierProvider(SecretsDataModel secrets)
        {
            this.secretsInfo = secrets;
        }
        public void Notify(List<AssetModel> assets)
        {
            // set twitter access token
            GetToken();
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
        
        public void GetToken()
        {

            //https://dev.twitter.com/oauth/application-only
            string strBearerRequest = HttpUtility.UrlEncode(
                this.secretsInfo.TwitterConnData.ApiKey)
                + ":" + HttpUtility.UrlEncode(this.secretsInfo.TwitterConnData.ApiSecret
            );
            
            strBearerRequest = System.Convert.ToBase64String(
                System.Text.Encoding.UTF8.GetBytes(strBearerRequest)
            );

            WebRequest request = WebRequest.Create("https://api.twitter.com/oauth2/token");
            request.Headers.Add("Authorization", "Basic " + strBearerRequest);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";

            string strRequestContent = "grant_type=client_credentials";
            byte[] bytearrayRequestContent = System.Text.Encoding.UTF8.GetBytes(strRequestContent);
            Stream requestStream = request.GetRequestStream();
            requestStream.Write(bytearrayRequestContent, 0, bytearrayRequestContent.Length);
            requestStream.Close();

            string responseJson = string.Empty;

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream responseStream = response.GetResponseStream();
                responseJson = new StreamReader(responseStream).ReadToEnd();
            }

            token = JsonSerializer.Deserialize<AccessToken>(responseJson);

        }
        private class AccessToken
        {
            [JsonPropertyName("access_token")]
            public string Value { get; set; }
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