using System.Collections.Generic;
namespace StockWatch.Assets
{
    public interface IAssetProcessor
    {
        List<AssetModel> GetAssets();
        int RemoveAssetsBelowTreshold(List<AssetModel> assets, Dictionary<string, AssetHistoryModel> assetsHistory);
    }

}