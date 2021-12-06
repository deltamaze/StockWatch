using System.Collections.Generic;
using StockWatch.Assets;
using StockWatch.Configuration;
using StockWatch.Data;
using StockWatch.Logging;

namespace StockWatch.Notifiers
{
    public class TwitterNotifierProvider : INotifierProvider
    {
        private SecretsDataModel secretsInfo;
        public TwitterNotifierProvider(SecretsDataModel secrets)
        {
            this.secretsInfo = secrets;
        }
        public void Notify(List<AssetModel> assets)
        {
            // compose message
            string composedMessage ="";
            bool skipInitialLinebreak = true;
            foreach(AssetModel asset in assets)
            {
                if(skipInitialLinebreak){
                    skipInitialLinebreak = false;
                }
                else
                {
                    composedMessage += "\n\n";
                }
                composedMessage +=
                (
                    $"Notify Stub for Asset {asset.Symbol}\n"+
                    $"Change Percent: {asset.PercentChange} etc.."
                );
            }

            // post
        }
    }
    
}