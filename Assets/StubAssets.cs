using System.Collections.Generic;

namespace StockWatch.Assets
{
    public class StubAssets : IAssets
    {
        public IEnumerable<AssetModel> GainingAssets()
        {
            List<AssetModel> returnAssets = new List<AssetModel>();
            returnAssets.Add(new AssetModel(){
                Company="Test1",
                Symbol="TST1",
                Url = "test1.com",
                MarketCap = 1000001.2M,
                PercentChange = 0.31M
            });
            returnAssets.Add(new AssetModel(){
                Company="Test2",
                Symbol="TST2",
                Url = "test2.com",
                MarketCap = 900002.1M,
                PercentChange = 0.24M
            });
            return returnAssets;
        }
        public IEnumerable<AssetModel> LosingAssets()
        {
            List<AssetModel> returnAssets = new List<AssetModel>();
            returnAssets.Add(new AssetModel(){
                Company="Test3",
                Symbol="TST3",
                Url = "test3.com",
                MarketCap = 999999M,
                PercentChange = -0.3M
            });
            returnAssets.Add(new AssetModel(){
                Company="Test4",
                Symbol="TST4",
                Url = "test4.com",
                MarketCap = 999999M,
                PercentChange = -0.29M
            });
            return returnAssets;
        }
    }
}