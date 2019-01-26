using System;
using System.Collections.Generic;

namespace Northwind_Product.Configuration
{
    /// <summary>
    /// XML Serialization mapping fields from Product.config
    /// </summary>
    public class MappingField
    {
        public MappingField()
        {
        }
        public string Column { get; set; }
        public string Label { get; set; }
        public string Key { get; set; }
        public bool Enabled { get; set; }
        public bool Display { get; set; }
        public bool Input { get; set; }
        public int Order { get; set; }       
        public string Type { get; set; }
        public bool Indexed { get; set; }
    }

    /// <summary>
    /// XML Serialization product configuration from Product.config
    /// </summary>
    public class ProductConfig
    {
        public ProductConfig()
        {
        }
        public string ListQuery { get; set; }
        public string NewQuery { get; set; }
        public string IndexKey { get; set; }
        public List<MappingField> Fields { get; set; }
    }
}