using StockWatch.Logging;
using StockWatch.Stocks;
namespace StockWatch
{

    class Application : IApplication
    {
        private ILoggingProcessor logger;
        public Application(ILoggingProcessor logger, IStocks stockApi)
        {
            this.logger = logger;
        }


        public void Run()
        {
            logger.Info("Starting Run");
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
