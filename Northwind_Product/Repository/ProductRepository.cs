using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

using Dapper;
using Northwind_Product.Models;
using Northwind_Product.Configuration;
using Northwind_Product.Utility;
using Serilog;

namespace Northwind_Product.Repository
{
    public interface IProductRepository
    {
        List<Product> GetProducts();
        List<dynamic> GetDynamicProducts();
        int AddProduct(Product product);
        int AddProduct(IDictionary<string, object> product);
    }

    /// <summary>
    /// Repository of Product to data and model layer
    /// </summary>
    public class ProductRepository : IProductRepository
    {
        private readonly ILogger _log;
        private readonly IConfig _store;

        private string connectionString = string.Empty;
        private ProductConfig config;

        public ProductRepository(ILogger log, IConfig store)
        {
            _log = log;
            _store = store;
            //SQL Azure etc
            connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            config = store.ProductConfig;            
        }

        /// <summary>
        /// Get dynamic list of Product based on Product.config
        /// </summary>
        /// <returns>Dynamic list of Product</returns>
        public List<dynamic> GetDynamicProducts()
        {
            var result = new List<dynamic>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    string selectSql = config.ListQuery;
                    result = db.Query<dynamic>(selectSql).ToList();                    
                }
                catch (Exception ex)
                {
                    _log.Error("GetDynamicProducts - {Message}\n{ex}", ex.Message, ex);
                }
                finally
                {
                    db.Close();
                }
            }

            return result;
        }
       
        /// <summary>
        /// Add new Product with dynamic mapped Dictionary based on Product.config
        /// </summary>
        /// <param name="product">Dynamic mapped Dictionary of Product</param>
        /// <returns></returns>
        public int AddProduct(IDictionary<string, object> product)
        {
            var result = 0;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    string insertSql = config.NewQuery;
                    result = db.Execute(insertSql, product);
                }
                catch (Exception ex)
                {
                    _log.Error("AddProduct - {Message}\n{ex}", ex.Message, ex);
                }
                finally
                {
                    db.Close();
                }
            }
            return result;
        }
        
        /// <summary>
        /// Get List of Product
        /// </summary>
        /// <returns>POCO list of Product</returns>
        public List<Product> GetProducts()
        {
            var result = new List<Product>();
            using (IDbConnection db = new SqlConnection(connectionString))
            {                               
                try
                {
                    db.Open();
                    string selectSql = "SELECT * FROM Products";
                    result = db.Query<Product>(selectSql).ToList();
                }
                catch(Exception ex)
                {
                    _log.Error("GetProducts - {Message}\n{ex}", ex.Message, ex);
                }
                finally
                {
                    db.Close();
                }
            }
            return result;
        }
     
        /// <summary>
        /// Add new Product
        /// </summary>
        /// <param name="product">POCO of Product</param>
        /// <returns></returns>
        public int AddProduct(Product product)
        {
            var result = 0;
            using (IDbConnection db = new SqlConnection(connectionString))
            {
                try
                {
                    db.Open();
                    string insertSql = @"INSERT INTO Products([ProductName], [QuantityPerUnit], [UnitPrice], [UnitsInStock]) VALUES (@ProductName, @QuantityPerUnit, @UnitPrice, @UnitsInStock)";
                    result = db.Execute(insertSql, product);
                }
                catch(Exception ex)
                {
                    _log.Error("AddProduct - {Message}\n{ex}", ex.Message, ex);
                }
                finally
                {
                    db.Close();
                }
            }
            return result;
        }
    }
}