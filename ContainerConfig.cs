using Autofac;
using StockWatch.Logging;
using StockWatch.Data;

namespace StockWatch
{
    public static class ContainerConfig
    {
        public static IContainer Configure(){
            var builder = new ContainerBuilder();
            builder.RegisterType<RunTimeData>().As<IRunTimeData>().SingleInstance();
            builder.RegisterType<ConsoleLogger>().As<ILogger>().SingleInstance();
            builder.RegisterType<LoggingProcessor>().As<ILoggingProcessor>().SingleInstance();
            builder.RegisterType<Application>().As<IApplication>();
            // builder.Register<ConsoleLogger>().As<ILogger>();
            return builder.Build();
        }
    }
}
