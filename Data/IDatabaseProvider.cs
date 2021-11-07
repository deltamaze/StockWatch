using System.Collections.Generic;
using System.Threading.Tasks;
using StockWatch.Assets;

namespace StockWatch.Data
{
    public interface IDatabaseProvider
    {
        Task ConnectToDatabase();
        Task<Dictionary<string,AssetHistoryModel>> GetHistory(List<AssetModel> asset);

        Task SaveHistory(List<AssetModel> asset);
    
    }
}