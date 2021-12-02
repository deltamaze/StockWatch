using System;
using System.Collections.Generic;

namespace StockWatch.Assets
{
    public class StubAssetsProvider : IAssetsProvider
    {
        public IEnumerable<AssetModel> GetAssets()
        {
            List<AssetModel> returnAssets = new List<AssetModel>();
            returnAssets.Add(new AssetModel(){
                Id = Guid.NewGuid().ToString(),
                Company="Test1",
                Symbol="TST1",
                Url = "test1.com",
                MarketCap = 6000000000.2M,
                PercentChange = 31M,
                AvgVolume = 31000000M,
                UnitPrice = 25.3M,
                ReportDate = System.DateTime.Now
                
            });
            returnAssets.Add(new AssetModel(){
                Id = Guid.NewGuid().ToString(),
                Company="Test2",
                Symbol="TST2",
                Url = "test2.com",
                MarketCap = 9000002.1M,
                PercentChange = 24M,
                AvgVolume = 3200000M,
                UnitPrice = 24M,
                ReportDate = System.DateTime.Now
            });
            returnAssets.Add(new AssetModel(){
                Id = Guid.NewGuid().ToString(),
                Company="Test3",
                Symbol="TST3",
                Url = "test3.com",
                MarketCap = 999999999M,
                PercentChange = -30M,
                AvgVolume = 3304235M,
                UnitPrice = 23M,
                ReportDate = System.DateTime.Now
            });
            returnAssets.Add(new AssetModel(){
                Id = Guid.NewGuid().ToString(),
                Company="Test4",
                Symbol="TST4",
                Url = "test4.com",
                MarketCap = 9995345999M,
                PercentChange = -29M,
                AvgVolume = 342000M,
                UnitPrice = 22M,
                ReportDate = System.DateTime.Now
            });
            return returnAssets;
        }
    }
}