using StockPile.Model.Inventory;
using StockPile.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Query.Handlers
{
    public interface IInventoryQryHandler :
        IQueryHandler<ProductQry>,
        IQueryHandler<ProductsQry>,
        IQueryHandler<BrandQry>,
        IQueryHandler<CategoryQry>
    { }

    public class InventoryQryHandler : IInventoryQryHandler
    {
        private IStockPileRepository _repository;
        public InventoryQryHandler(IStockPileRepository repository)
        {
            _repository = repository;
        }

        public object Handle(ProductQry query)
        {
            var data = _repository.GetProduct(query.Id);
            return data;
        }

        public object Handle(ProductsQry query)
        {
            var data = _repository.GetProducts(query.Name, query.Brand, query.Category);
            return data;
        }

        public object Handle(BrandQry query)
        {
            var data = _repository.GetBrands;
            return data;
        }

        public object Handle(CategoryQry query)
        {
            var data = _repository.GetCategories;
            return data;
        }
    }
}
