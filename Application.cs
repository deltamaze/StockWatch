using System.Collections.Generic;
using StockWatch.Assets;
using StockWatch.Data;
using StockWatch.Logging;
using StockWatch.Notifiers;


namespace StockWatch
{

    class Application : IApplication
    {
        private ILoggingProcessor logger;
        private IAssetProcessor assetProcessors;
        private RunTimeData runData;
        private IDatabaseProvider dbProvider;
        private INotifierProcessor notifierProcessor;
        public Application(ILoggingProcessor logger,
            IAssetProcessor assetProcessors,
            RunTimeData runData,
            IDatabaseProvider dbProvider,
            INotifierProcessor notifierProcessor
        )
        {
            this.logger = logger;
            this.assetProcessors = assetProcessors;
            this.runData = runData;
            this.dbProvider = dbProvider;
            this.notifierProcessor = notifierProcessor;
        }


        public void Run()
        {
            logger.Info("Starting Run");
            runData.Assets = assetProcessors.GetAssets();
            // Get the history of each asset
            runData.AssetHistory = dbProvider.GetHistory(runData.Assets);
            // Remove stocks that don’t hit requirements (percent change over a certain amount, not recently reported, etc..)
            assetProcessors.RemoveAssetsBelowTreshold(runData.Assets,runData.AssetHistory);
            // Save new stocks to DB
            dbProvider.SaveHistory(runData.Assets);
            // Post output to final end points
            notifierProcessor.Notify(runData.Assets);
            logger.Info("Ending Run");

        }
    }
}
