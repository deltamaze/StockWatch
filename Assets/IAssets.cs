using System.Collections.Generic;

namespace StockWatch.Assets
{
    public interface IAssets
    {
        IEnumerable<AssetModel> GainingAssets();
        IEnumerable<AssetModel> LosingAssets();
    }
}