using System.Collections.Generic;

namespace StockWatch.Assets
{
    public interface IAssetsProvider
    {
        IEnumerable<AssetModel> GetAssets();
    }
}