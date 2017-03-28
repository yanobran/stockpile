using System;

namespace StockPile.Query.Handlers
{
    public interface IQueryHandler<Q> where Q : IQuery
    {
        object Handle(Q query);
    }
}
