using StockPile.Model;
using System;
using System.Linq;
using System.Text;

namespace StockPile.Repository
{
    public interface IStockPileRepository : IInventoryRepository, IFulfillmentRepository
    {
    }
}
