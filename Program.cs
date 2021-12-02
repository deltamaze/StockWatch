using System;
using Autofac;

namespace StockWatch
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var container = ContainerConfig.Configure();

                using(var scope = container.BeginLifetimeScope()){
                    var app= scope.Resolve<IApplication>();
                    app.Run().Wait();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Unhandled Exception");
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
