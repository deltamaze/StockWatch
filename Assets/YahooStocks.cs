using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System;
using System.Text.Json;
using System.Threading.Tasks;


namespace StockWatch.Assets
{
    public class YahooStocks : IAssets
    {
        private const string GainingStocksUrl = "https://query1.finance.yahoo.com/v1/finance/screener/predefined/saved?formatted=true&lang=en-US&region=US&scrIds=day_gainers&start=0&count=10";
        private const string LosingStocksUrl =  "https://query2.finance.yahoo.com/v1/finance/screener/predefined/saved?formatted=true&lang=en-US&region=US&scrIds=day_losers&start=0&count=10";

        private static readonly HttpClient client = new HttpClient();
        public YahooStocks()
        {
        }
        public IEnumerable<AssetModel> GainingAssets()
        {

            WebClient client = new WebClient();
            string reply = client.DownloadString(GainingStocksUrl);
            YahooStockModel parsedReply = JsonSerializer.Deserialize<YahooStockModel>(reply);



            throw new System.Exception("Not Implemented");
        }
        public IEnumerable<AssetModel> LosingAssets()
        {
            WebClient client = new WebClient();
            string reply = client.DownloadString(LosingStocksUrl);
            YahooStockModel parsedReply = JsonSerializer.Deserialize<YahooStockModel>(reply);

            throw new System.Exception("Not Implemented");
        }
    }
}