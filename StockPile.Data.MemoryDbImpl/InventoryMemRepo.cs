using StockPile.Repository;
using System;
using StockPile.Model.Inventory;
using System.Collections.Generic;
using System.Linq;
using StockPile.Repository.Let;
using Microsoft.Extensions.Options;
using StockPile.Model.Fulfillment;

namespace StockPile.Data.MemoryDbImpl
{
    public class InventoryMemRepo : IStockPileRepository
    {
        Dictionary<Guid, Product> _products;
        Dictionary<Guid, Brand> _brands;
        List<string> _categories;
        Dictionary<Guid, Order> _orders;
        
        public InventoryMemRepo(IOptions<MemoryDbConfig> options)
        {
            string datapath = options.Value.DataPath ?? "";

            _categories = bootstrap.InitInventory.GetCategories(datapath);
            _brands = bootstrap.InitInventory.GetBrands(datapath);
            _products = bootstrap.InitInventory.GetProducts(datapath);
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

        public IEnumerable<Order> GetOrders(Guid userId) => this._orders.Values.ToList();

        public void AddOrder(Order order)
        {
            if (this._orders.ContainsKey(order.Id))
                throw new Exception("Cannot add order. Order with same ID already exists.");

            this._orders.Add(order.Id, order);
        }

        public void ProcessOrder(Guid orderId)
        {
            if(!this._orders.ContainsKey(orderId))
                throw new Exception("Cannot process order, order does not exist.");

            var order = this._orders[orderId];
            order.Status = OrderStatus.Complete;
            this._orders[orderId] = order;
        }
    }
}
