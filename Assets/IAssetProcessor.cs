using System.Collections.Generic;
namespace StockWatch.Assets
{
    public interface IAssetProcessor
    {
        List<AssetModel> GetAssets();
        void RemoveAssetsBelowTreshold(List<AssetModel> assets, Dictionary<string, AssetHistoryModel> assetsHistory);
    }

}