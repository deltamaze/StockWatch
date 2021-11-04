using System.Collections.Generic;
using StockWatch.Assets;

namespace StockWatch.Data
{
    public interface IDatabaseProvider
    {
        Dictionary<string,AssetHistoryModel> GetHistory(List<AssetModel> asset);

        void SaveHistory(List<AssetModel> asset);
    
    }
}