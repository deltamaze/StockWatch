using System.Collections.Generic;
using StockWatch.Assets;

namespace StockWatch.Notifiers
{

    public class NotifierProcessor : INotifierProcessor
    {
        private INotifierProvider[] providers;
        public NotifierProcessor(INotifierProvider[] providers)
        {
            this.providers = providers;
        }

        public void Notify(List<AssetModel> assets)
        {
            foreach (INotifierProvider provider in providers)
            {
                provider.Notify(assets);
            }
        }

    }

}