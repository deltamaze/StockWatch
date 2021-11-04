using System.Collections.Generic;
using StockWatch.Assets;

namespace StockWatch.Notifiers
{
    public interface INotifierProvider
    {
        void Notify(List<AssetModel> assets);
    }
    
}