using System;

namespace StockPile.Model.Inventory
{
    public class Brand : IHasKey<Guid>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
