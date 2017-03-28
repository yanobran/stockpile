using StockPile.Model.Fulfillment;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Repository
{
    public interface IFulfillmentRepository
    {
        IEnumerable<Order> GetOrders(Guid userId);
        void AddOrder(Order order);
        void ProcessOrder(Guid orderId);
    }
}
