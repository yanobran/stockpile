using StockPile.Model.Fulfillment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockPile.Repository.Let
{
    public static class FulfillmentLetHelpers
    {
        public static IQueryable<Order> WithUser(this IQueryable<Order> query, Guid userId)
        {
            var filter = from o in query
                         where o.UserId == userId
                         select o;
            return filter;
        }

        public static IQueryable<Order> WithId(this IQueryable<Order> query, Guid? id)
        {
            if (!id.HasValue)
                return query;

            var filter = from o in query
                         where o.Id == id.Value
                         select o;

            return filter;
        }
    }
}
