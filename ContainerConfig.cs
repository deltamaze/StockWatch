using Autofac;
using StockWatch.Assets;
using StockWatch.Data;
using StockWatch.Logging;
using StockWatch.Notifiers;

namespace StockWatch
{
    public static class ContainerConfig
    {
        public static IContainer Configure(){
            // config
            // yahooStockApi
            // 
            

            var builder = new ContainerBuilder();
            builder.RegisterType<RunTimeData>().SingleInstance();
            builder.RegisterType<ConsoleLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<LoggingProcessor>().As<ILoggingProcessor>().SingleInstance();
            builder.RegisterType<YahooStocks>().As<IAssetsProvider>().SingleInstance();
            builder.RegisterType<AssetProcessor>().As<IAssetProcessor>().SingleInstance();
            builder.RegisterType<NotifierProcessor>().As<INotifierProcessor>().SingleInstance();
            builder.RegisterType<Application>().As<IApplication>();
            // builder.Register<ConsoleLogger>().As<ILogger>();
            return builder.Build();
        }
    }
}
