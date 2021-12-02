using System;
using System.Collections.Generic;

namespace StockWatch.Assets
{
    public class AssetHistoryModel
    {
        public DateTime LastEntry { get; set; }
        public List<AssetModel> HistoryEntries { get; set; }
    }
    
}