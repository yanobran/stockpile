using StockPile.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockPile.Repository.Let
{
    public static class ProductsLetHelpers
    {
        public static IQueryable<Product> WithNameLike(this IQueryable<Product> query, string name)
        {
            if (string.IsNullOrEmpty(name))
                return query;

            var filter = from p in query
                         where p.Name.ToLower().Contains(name.ToLower())
                         select p;
            return filter;
        }

        public static IQueryable<Product> WithCategory(this IQueryable<Product> query, string category)
        {
            if (string.IsNullOrEmpty(category))
                return query;

            var filter = from p in query
                         where p.Category.ToLower() == category.ToLower()
                         select p;

            return filter;
        }

        public static IQueryable<Product> WithBrand(this IQueryable<Product> query, Guid? brand)
        {
            if (!brand.HasValue)
                return query;

            var filter = from p in query
                         where p.Brand == brand
                         select p;

            return filter;
        }
    }
}
