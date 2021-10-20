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

        public IEnumerable<AssetModel> GainingAssets()
        {
            YahooStockModel assets = GetYahooStocks(GainingStocksUrl);
            return ParseYahooStocks(assets);
        }
        public IEnumerable<AssetModel> LosingAssets()
        {
            YahooStockModel assets = GetYahooStocks(LosingStocksUrl);
            return ParseYahooStocks(assets);
        }
        private YahooStockModel GetYahooStocks(string url)
        {
            WebClient client = new WebClient();
            string reply = client.DownloadString(url);
            return JsonSerializer.Deserialize<YahooStockModel>(reply);
        }
        private List<AssetModel> ParseYahooStocks(YahooStockModel yahooStocks)
        {
            List<AssetModel> returnList = new List<AssetModel>();
            // null checks
            if(
                yahooStocks == null ||
                yahooStocks.finance == null ||
                yahooStocks.finance.result == null ||
                yahooStocks.finance.result.Count < 1 ||
                yahooStocks.finance.result[0].quotes == null)
            {
                return returnList;
            }
            foreach(var quote in yahooStocks.finance.result[0].quotes)
            {
                AssetModel stock = new AssetModel();
                stock.Company = quote.displayName;
                stock.Symbol = quote.symbol;
                stock.MarketCap = decimal.Parse(quote.marketCap.longFmt.Replace(",",""));
                stock.PercentChange = ((decimal)quote.regularMarketChangePercent.raw);
                returnList.Add(stock);
            }
            return returnList;
            
        }
    }
}