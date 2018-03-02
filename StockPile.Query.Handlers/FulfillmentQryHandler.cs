using StockPile.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Query.Handlers
{
    public interface IFulfillmentQryHandler :
        IQueryHandler<OrderQry>,
        IQueryHandler<OrdersQry>
    { }

    public class FulfillmentQryHandler : IFulfillmentQryHandler
    {
        private readonly IFulfillmentRepository _repository;

        public FulfillmentQryHandler(IFulfillmentRepository repository)
        {
            _repository = repository;
        }

        public object Handle(OrderQry query)
        {
            var data = _repository.GetOrders(query.UserId.Value, query.Id);
            return data;
        }

        public object Handle(OrdersQry query)
        {
            throw new NotImplementedException();
        }
    }
}
