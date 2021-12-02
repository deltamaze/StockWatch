using System;
using System.Collections.Generic;
using System.Linq;

namespace StockWatch.Assets
{

    public class AssetProcessor : IAssetProcessor
    {
        IAssetsProvider[] providers;
        public AssetProcessor(IAssetsProvider[] assetProviders)
        {
            this.providers = assetProviders;
        }
        public List<AssetModel> GetAssets()
        {
            List<AssetModel> assets = new List<AssetModel>();
            foreach (IAssetsProvider provider in providers)
            {
                assets.AddRange(provider.GetAssets());
            }
            return assets;
        }
        public int RemoveAssetsBelowTreshold(List<AssetModel> assets)
        {
            int assetsLen = assets.Count();
            int removeCount = 0;
            for (int x = assetsLen - 1; x >= 0; x -= 1)
            {
                bool keepAsset = CheckPercentChange(assets[x]) ||
                    CheckMarketCap(assets[x]) ||
                    CheckAvgVol(assets[x]);
                if (!keepAsset)
                {
                    removeCount +=1;
                    assets.RemoveAt(x);
                }
            }
            return removeCount;
        }
        public int RemoveFromRecentReporting(List<AssetModel> assets,
            Dictionary<string, AssetHistoryModel> assetsHistory)
        {
            int assetsLen = assets.Count();
            int removeCount = 0;
            for (int x = assetsLen - 1; x >= 0; x -= 1)
            {
                bool keepAsset = 
                    CheckHistory(assetsHistory[assets[x].Symbol]);
                if (!keepAsset)
                {
                    removeCount +=1;
                    assets.RemoveAt(x);
                }
            }
            return removeCount;
        }
        private bool CheckPercentChange(AssetModel asset)
        {
            if (asset.PercentChange > 20 || asset.PercentChange < -20)
            {
                return true;
            }
            return false;
        }

        private bool CheckMarketCap(AssetModel asset)
        {
            if (asset.MarketCap > 5000000000)
            {
                return true;
            }
            return false;
        }

        private bool CheckAvgVol(AssetModel asset)
        {
            if (asset.AvgVolume > 100000)
            {
                return true;
            }
            return false;
        }

        private bool CheckHistory(AssetHistoryModel assetsHistory)
        {
            if(DateTime.Compare(assetsHistory.LastEntry,
                System.DateTime.Now.AddDays(-3))<0)
            {
                // last entry is earlier than 3 days ago, so good to re-report
                return true;
            }
            return false;
        }


    }

}