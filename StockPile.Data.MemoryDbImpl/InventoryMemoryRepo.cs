using StockPile.Repository;
using System;
using StockPile.Model.Inventory;
using System.Collections.Generic;
using System.Linq;
using StockPile.Repository.Let;
using Microsoft.Extensions.Options;
using StockPile.Model.Fulfillment;
using StockPile.Model;

namespace StockPile.Data.MemoryDbImpl
{
    public class InventoryMemoryRepo : IInventoryRepository
    {
        readonly Dictionary<Guid, Product> _products;
        readonly Dictionary<Guid, Brand> _brands;
        readonly List<string> _categories;
        
        public InventoryMemoryRepo(IOptions<MemoryDbConfig> options)
        {
            string datapath = options.Value.DataPath ?? "";

            _categories = InitInventory.GetCategories(datapath);
            _brands = InitInventory.GetBrands(datapath);
            _products = InitInventory.GetProducts(datapath);

        }

        public IEnumerable<string> GetCategories => this._categories;

        public IEnumerable<Brand> GetBrands => this._brands.Values.ToList();

        /// <summary>
        /// NOTE - With*** expressions below are custom layered expression trees in the StockPile.Repository 'Let' folder 
        ///        which allow for reusable query filters.
        /// </summary>
        public IEnumerable<Product> GetProducts(string name = null, Guid? brand = null, string category = null)
        {
            var data = from p in _products.Values.AsQueryable<Product>()
                       .WithNameLike(name)
                       .WithCategory(category)
                       .WithBrand(brand)
                       select p;
                       
            return data.ToList();
        }

        public Product GetProduct(Guid id)
        {
            return _products[id] ?? null;
        }
    }
}
