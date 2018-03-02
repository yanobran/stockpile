using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Query
{
    public struct OrderQry : IQuery
    {
        public OrderQry(Guid? id, Guid? userId)
        {
            this.Id = id;
            this.UserId = userId;
        }
        public Guid? Id { get; set; }
        public Guid? UserId;
    }

    public struct OrdersQry : IQuery
    {
        public OrdersQry(Guid? userId = null)
        {
            this.UserId = userId;
        }
        public Guid? UserId;
    }
}
