using System;
using System.Collections.Generic;
using System.Dynamic;
using NUnit.Framework;
using Northwind_Product.Repository;
using Northwind_Product.Models;

namespace Northwind_Product.Tests
{
    [TestFixture]
    public class ProductRepositoryTest : TestBase
    {
        [Test]
        public void Test_GetDynamicProduct_Any()
        {
            var repository = this.Resolve<IProductRepository>();
            var products = repository.GetDynamicProducts();
            var product = products[0];

            Assert.That(products.Count > 0);

            Assert.Positive(product.ProductID);
            Assert.IsNotEmpty(product.ProductName);
            Assert.IsNotEmpty(product.QuantityPerUnit);
            Assert.Positive(product.UnitPrice);
            Assert.Positive(product.UnitsInStock);
        }

        [Test]
        public void Test_GetProduct_Any()
        {
            var repository = this.Resolve<IProductRepository>();
            var products = repository.GetProducts();
            var product = products[0];

            Assert.That(products.Count > 0);

            Assert.Positive(product.ProductID);
            Assert.IsNotEmpty(product.ProductName);
            Assert.IsNotEmpty(product.QuantityPerUnit);
            Assert.Positive(product.UnitPrice.Value);
            Assert.Positive(product.UnitsInStock.Value);
        }

        [Test]
        public void Test_AddDynamicProduct()
        {
            var repository = this.Resolve<IProductRepository>();
            dynamic item = new ExpandoObject();
            item.ProductName = "Test Product Name " + DateTime.Now.ToString();
            item.QuantityPerUnit = "Test Qty Per Unit";
            item.UnitPrice = 2.99;
            item.UnitsInStock = 22;            

            IDictionary<string, object> product = item;
            var result = repository.AddProduct(product);

            Assert.That(result > 0);            
        }

        [Test]
        public void Test_AddProduct()
        {
            var repository = this.Resolve<IProductRepository>();
            var product = new Product()
            {
                ProductName = "Test Product Name " + DateTime.Now.ToString(),
                QuantityPerUnit = "Test Qty Per Unit",
                UnitPrice = 3.99m,
                UnitsInStock = 33,
            };
            var result = repository.AddProduct(product);

            Assert.That(result > 0);
        }
    }
}
