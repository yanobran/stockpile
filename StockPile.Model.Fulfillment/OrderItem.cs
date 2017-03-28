using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Model.Fulfillment
{
    public class OrderItem
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
