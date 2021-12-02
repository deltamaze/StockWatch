using System.Collections.Generic;
using StockWatch.Assets;
using StockWatch.Logging;

namespace StockWatch.Notifiers
{
    public class StubNotifierProvider : INotifierProvider
    {
        private ILoggingProcessor logger;
        public StubNotifierProvider(ILoggingProcessor logger)
        {
            this.logger = logger;
        }
        public void Notify(List<AssetModel> assets)
        {
            foreach(AssetModel asset in assets)
            {
                logger.Info
                (
                    $"Notify Stub for Asset {asset.Symbol}\n"+
                    $"Change Percent: {asset.PercentChange} etc.."
                );
            }
        }
    }
    
}