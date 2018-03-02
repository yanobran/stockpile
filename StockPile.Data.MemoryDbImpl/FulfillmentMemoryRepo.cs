using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using StockPile.Model;
using StockPile.Model.Fulfillment;
using StockPile.Repository;

namespace StockPile.Data.MemoryDbImpl
{
    public class FulfillmentMemoryRepo : IFulfillmentRepository
    {
        readonly Dictionary<Guid, Order> _orders;
        readonly Dictionary<Guid, User> _users;

        public FulfillmentMemoryRepo(IOptions<MemoryDbConfig> options)
        {
            string datapath = options.Value.DataPath ?? "";

            _users = InitUsers.GetUsers(datapath);
            _orders = new Dictionary<Guid, Order>();
        }

        public void AddOrder(Order order)
        {
            if (this._orders.ContainsKey(order.Id))
                throw new Exception("Cannot add order. Order with same ID already exists.");

            this._orders.Add(order.Id, order);
        }

        public IEnumerable<Order> GetOrders(Guid userId, Guid? id)
        {
            throw new NotImplementedException();
        }

        public void ProcessOrder(Guid orderId)
        {
            if (!this._orders.ContainsKey(orderId))
                throw new Exception("Cannot process order, order does not exist.");

            var order = this._orders[orderId];
            order.Status = OrderStatus.Complete;
            this._orders[orderId] = order;
        }
    }
}
