using StockPile.Model.Inventory;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace StockPile.Repository
{
    public interface IInventoryRepository
    {
        IEnumerable<Product> GetProducts(string name = null, Guid? brand = null, string category = null);
        Product GetProduct(Guid id);
        IEnumerable<string> GetCategories { get; }
        IEnumerable<Brand> GetBrands { get; }
    }
}
