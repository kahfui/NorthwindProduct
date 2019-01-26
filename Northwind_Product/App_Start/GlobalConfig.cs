using System;
using System.Web;

using Northwind_Product.Configuration;
using Northwind_Product.Utility;

namespace Northwind_Product
{
    /// <summary>
    /// Loading Product specific configuration for caching
    /// </summary>
    public class GlobalConfig
    {
        /// <summary>
        /// Load configuration from Product.config
        /// </summary>
        public static void LoadConfiguration()
        {
            var productConfig = XmlSerialization.ReadFromXmlFile<ProductConfig>(HttpContext.Current.Server.MapPath("~/Configuration/Product.config"));
            HttpContext.Current.Cache["ProductConfig"] = productConfig;
        }

        /// <summary>
        /// Get product configuration from caching, Product.config
        /// </summary>
        /// <returns></returns>
        public static ProductConfig GetProductConfig()
        {
            var productConfig = HttpContext.Current.Cache["ProductConfig"] as ProductConfig;
            return productConfig;
        }
    }

    public interface IConfig
    {
        ProductConfig ProductConfig { get; }
    }

    /// <summary>
    /// Configuration store for product config
    /// </summary>
    public class ConfigStore : IConfig
    {
        public ProductConfig ProductConfig
        {
            get
            {
                return GlobalConfig.GetProductConfig();
            }
        }
    }
}