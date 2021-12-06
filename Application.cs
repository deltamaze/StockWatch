using System.Collections.Generic;
using System.Threading.Tasks;
using StockWatch.Assets;
using StockWatch.Configuration;
using StockWatch.Data;
using StockWatch.Logging;
using StockWatch.Notifiers;


namespace StockWatch
{

    class Application : IApplication
    {
        private ILoggingProcessor logger;
        private IAssetProcessor assetProcessors;
        private RunTimeDataModel runData;
        private IDatabaseProvider dbProvider;
        private INotifierProcessor notifierProcessor;
        private ISecretProcessor secretProcessor;
        public Application(ILoggingProcessor logger,
            IAssetProcessor assetProcessors,
            RunTimeDataModel runData,
            IDatabaseProvider dbProvider,
            INotifierProcessor notifierProcessor,
            ISecretProcessor secretProcessor
        )
        {
            this.logger = logger;
            this.assetProcessors = assetProcessors;
            this.runData = runData;
            this.dbProvider = dbProvider;
            this.notifierProcessor = notifierProcessor;
            this.secretProcessor = secretProcessor;
        }


        public async Task<int> Run()
        {

            logger.Info("Starting Run");
            logger.Info("Load Secrets from json");
            secretProcessor.LoadSecrets();

            logger.Info("Connect to Database");
            await dbProvider.ConnectToDatabase();

            logger.Info("Get Recent Assets Activity From Web Endpoints");
            runData.Assets = assetProcessors.GetAssets();
            if(runData.Assets.Count == 0)
            {
                logger.Info("No Assets pulled, ending run");
                return 1;
            }
            logger.Info($"Assets pulled: {runData.Assets.Count}");


            logger.Info("Clear Assets that don't meet requirements");
            int removeCount = assetProcessors.RemoveAssetsBelowTreshold(runData.Assets);
            logger.Info($"Assets Removed:{removeCount} Assets Remaining:{runData.Assets.Count}");
            
            if(runData.Assets.Count == 0)
            {
                logger.Info("No Assets left, ending run");
                return 2;
            }

            logger.Info("Pull History For these Assets from our Database");
            runData.AssetHistory = await dbProvider.GetHistory(runData.Assets);
            logger.Info($"Historical Records pulled:{runData.AssetHistory.Count}");

            logger.Info("Remove Assets that have been recently reported on");
            removeCount = assetProcessors.RemoveFromRecentReporting(
                runData.Assets,
                runData.AssetHistory);
            logger.Info($"Assets Removed:{removeCount} Assets Remaining:{runData.Assets.Count}");
            
            if(runData.Assets.Count == 0)
            {
                logger.Info("No Assets left, ending run");
                return 3;
            }

            logger.Info("Save the remaining assets into the DB");
            await dbProvider.SaveHistory(runData.Assets);
            
            logger.Info("Broadcast Alerts to Web Endpoints");
            notifierProcessor.Notify(runData.Assets);
            logger.Info("Ending Run");

            return 4;

        }
    }
}
