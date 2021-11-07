using System.Linq;
using System.Collections.Generic;

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
        public int RemoveFromRecentReporting(List<AssetModel> assets, Dictionary<string, AssetHistoryModel> assetsHistory)
        {
            int assetsLen = assets.Count();
            int removeCount = 0;
            for (int x = assetsLen - 1; x >= 0; x -= 1)
            {
                bool keepAsset = CheckPercentChange(assets[x]) ||
                    CheckMarketCap(assets[x]) ||
                    CheckAvgVol(assets[x]) ||
                    CheckHistory(assets[x], assetsHistory);
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

        private bool CheckHistory(AssetModel asset, Dictionary<string, AssetHistoryModel> assetsHistory)
        {
            throw new System.Exception("Not Implemented");
        }


    }

}