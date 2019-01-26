using System;
using Unity;
using Serilog;
using Northwind_Product.Configuration;
using Northwind_Product.Repository;
using Northwind_Product.Utility;

namespace Northwind_Product.Tests
{
    public class TestBase
    {
        private static IUnityContainer _unityContainer = null;
        private static IUnityContainer UnityContainer
        {
            get { return _unityContainer ?? (_unityContainer = GetUnityContainer()); }
        }

        /// <summary>
        /// Get main unity container
        /// </summary>
        /// <returns></returns>
        private static IUnityContainer GetUnityContainer()
        {
            var container = new UnityContainer();

            //Register Components
            container.RegisterType<IProductRepository, ProductRepository>();
            container.RegisterType<IConfig, TestConfigStore>(); //switch to TestConfigStore for Testing only
            container.RegisterInstance<ILogger>(new LoggerConfiguration().WriteTo.RollingFile(System.AppDomain.CurrentDomain.BaseDirectory + @"\Logs\{Date}.log").CreateLogger());

            TestGlobalConfig.LoadConfiguration();

            return container;
        }

        /// <summary>
        /// Resolve unity registered types
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected T Resolve<T>()
        {
            return UnityContainer.Resolve<T>();
        }

    }

    /// <summary>
    /// For Tests only as Dependency Injection
    /// </summary>
    public class TestConfigStore : IConfig
    {
        public ProductConfig ProductConfig
        {
            get
            {
                return TestGlobalConfig.GetProductConfig();
            }
        }
    }

    /// <summary>
    /// For Tests only
    /// </summary>
    public class TestGlobalConfig
    {
        private static ProductConfig _productConfig;
        public static void LoadConfiguration()
        {
            var configPath = System.AppDomain.CurrentDomain.BaseDirectory + @"Product.config";
            var productConfig = XmlSerialization.ReadFromXmlFile<ProductConfig>(configPath);
            _productConfig = productConfig;
        }

        public static ProductConfig GetProductConfig()
        {
            return _productConfig;
        }
    }
}
