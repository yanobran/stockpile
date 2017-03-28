using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Query
{
    public struct ProductQry : IQuery
    {
        public ProductQry(Guid id)
        {
            this.Id = id;
        }
        public Guid Id { get; set; }
    }

    public struct ProductsQry : IQuery
    {
        public ProductsQry(string name = null, string category = null, Guid? brand = null)
        {
            this.Name = name;
            this.Brand = brand;
            this.Category = category;
        }
        public string Name;
        public string Category;
        public Guid? Brand;
    }

    public struct BrandQry : IQuery
    {

    }

    public struct CategoryQry : IQuery
    {

    }
}
