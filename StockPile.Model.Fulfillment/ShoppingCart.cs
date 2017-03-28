using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Model.Fulfillment
{
    public class ShoppingCart
    {
        public Dictionary<Guid, int> Products { get; set; }
        public Guid? UserId { get; set; }
    }
}
