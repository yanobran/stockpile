using System;

namespace StockPile.Model
{
    public interface IHasKey<TKey>
    {
        TKey Id { get; set; }
    }
}
