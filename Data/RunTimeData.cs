using System.Collections.Generic;

using StockWatch.Assets;

namespace StockWatch.Data
{
    public class RunTimeData
    {
        public List<AssetModel> Assets { get; set; }
        public Dictionary<string,AssetHistoryModel> AssetHistory { get; set; }
    }

}