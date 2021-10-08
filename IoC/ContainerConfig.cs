using System;
using Autofac;

namespace StockWatch.IoC
{
    public static class ContainerConfig
    {
        public static IContainer Configure(){
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>().As<IApplication>();
            // builder.Register<ConsoleLogger>().As<ILogger>();
            return builder.Build();
        }
    }
}
