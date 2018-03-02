using System;
using System.Collections.Generic;

namespace StockPile.Model.Fulfillment
{
    public class Order
    {
        public Guid Id { get; set; }
        public OrderStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }

        //Relationships/Keys
        public Guid UserId { get; set; } = Guid.Empty;
        public List<OrderItem> Items { get; set; }
    }
}
