using System;
using StockPile.Model;

namespace StockPile.Model.Inventory
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public string ImageUrl { get; set; }
        
        // Relationships/Keys
        public Guid Brand { get; set; }
        public string Category { get; set; }
        public Stock Stock { get; set; }
    }
}
