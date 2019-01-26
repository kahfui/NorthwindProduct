using System;
using System.Collections.Generic;
using System.Web.Http;

using Newtonsoft.Json.Linq;
using Northwind_Product.Models;
using Northwind_Product.Repository;

namespace Northwind_Product.API
{
    public class ProductsController : ApiController
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Get dynamic list of Product for JSON binding
        /// </summary>
        /// <returns>Dynamic list of Product</returns>
        [HttpGet]
        [Route("api/AllProducts")]        
        public List<dynamic> GetAllProducts()
        {            
            return _productRepository.GetDynamicProducts();
        }

        /// <summary>
        /// Add new Product from dynamic JSON
        /// </summary>
        /// <param name="item">JSON of Product</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AddDynamicProduct")]
        public string AddProduct(JObject data)
        {            
            var item = data.ToObject<IDictionary<string, object>>();
            return _productRepository.AddProduct(item).ToString();
        }

        /// <summary>
        /// Get list of Product
        /// </summary>
        /// <returns>POCO list of Product</returns>
        [HttpGet]
        [Route("api/Products")]
        public List<Product> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        /// <summary>
        /// Add new Product
        /// </summary>
        /// <param name="item">POCO of Product</param>
        /// <returns></returns>
        [HttpPost]
        [Route("api/AddProduct")]
        public string AddProduct(Product item)
        {
            return _productRepository.AddProduct(item).ToString();
        }
    }
}
