using Unity;
using System.Web.Http;
using System.Web.Mvc;

using Northwind_Product.Repository;
using Serilog;

namespace Northwind_Product
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
            var container = new UnityContainer();

            // register all your components with the container here
            // it is NOT necessary to register your controllers

            // e.g. container.RegisterType<ITestService, TestService>();            

            container.RegisterType<IProductRepository, ProductRepository>();

            container.RegisterType<IConfig, ConfigStore>();
                        
            container.RegisterInstance<ILogger>(new LoggerConfiguration().WriteTo.RollingFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\Logs\{Date}.log").CreateLogger());

            DependencyResolver.SetResolver(new Unity.Mvc5.UnityDependencyResolver(container));

            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);            
        }
    }
}