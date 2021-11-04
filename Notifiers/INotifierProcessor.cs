using System.Collections.Generic;
using StockWatch.Assets;

namespace StockWatch.Notifiers
{
    public interface INotifierProcessor
    {
        void Notify(List<AssetModel> assets);
    }

}