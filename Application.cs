using System.Collections.Generic;
using StockWatch.Logging;
using StockWatch.Assets;


namespace StockWatch
{

    class Application : IApplication
    {
        private ILoggingProcessor logger;
        private IAssets[] assetProviders;
        public Application(ILoggingProcessor logger, IAssets[] assetProviders)
        {
            this.logger = logger;
            this.assetProviders = assetProviders;
        }


        public void Run()
        {
            logger.Info("Starting Run");
            List<AssetModel> assets = new List<AssetModel>();
            foreach(var assetProvider in assetProviders){
                assets.AddRange(assetProvider.GainingAssets());
            }
            foreach(var assetProvider in assetProviders){
                assets.AddRange(assetProvider.LosingAssets());
            }
            
            // Pull stocks into context
            // Pull top stocks
            // Pull bad performing stocks
            // Remove stocks that don’t hit requirements
            // Pull History
            // Remove Stocks that were too recently posted
            // Save new stocks to DB
            // Post stocks to all IStockWatchOutputs[]
            logger.Info("Ending Run");

        }
    }
}
