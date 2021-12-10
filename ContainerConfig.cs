using Autofac;
using StockWatch.Assets;
using StockWatch.Configuration;
using StockWatch.Data;
using StockWatch.Logging;
using StockWatch.Notifiers;

namespace StockWatch
{
    public static class ContainerConfig
    {
        public static IContainer Configure(){
            var builder = new ContainerBuilder();
            builder.RegisterType<RunTimeDataModel>().SingleInstance();
            builder.RegisterType<SecretsDataModel>().SingleInstance();
            builder.RegisterType<ConsoleLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<LoggingProcessor>().As<ILoggingProcessor>().SingleInstance();
            builder.RegisterType<CosmosDbSecretLoader>().As<ISecretLoader>().SingleInstance();
            builder.RegisterType<TwitterSecretLoader>().As<ISecretLoader>().SingleInstance();
            builder.RegisterType<SecretProcessor>().As<ISecretProcessor>().SingleInstance();
            // builder.RegisterType<YahooStocks>().As<IAssetsProvider>().SingleInstance();
            builder.RegisterType<StubAssetsProvider>().As<IAssetsProvider>().SingleInstance();
            builder.RegisterType<AssetProcessor>().As<IAssetProcessor>().SingleInstance();
            // builder.RegisterType<StubNotifierProvider>().As<INotifierProvider>().SingleInstance();
            builder.RegisterType<TwitterNotifierProvider>().As<INotifierProvider>().SingleInstance();
            builder.RegisterType<NotifierProcessor>().As<INotifierProcessor>().SingleInstance();
            builder.RegisterType<CosmosDatabaseProvider>().As<IDatabaseProvider>().SingleInstance();
            builder.RegisterType<Application>().As<IApplication>();
            // builder.Register<ConsoleLogger>().As<ILogger>();
            return builder.Build();
        }
    }
}
