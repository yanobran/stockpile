using System;
using System.Collections.Generic;
using StockPile.Repository;
using StockPile.Model.Fulfillment;
using StockPile.Messaging;

namespace StockPile.Command.Handlers
{
    public interface IFulfillmentCmdHandler :
        IMessageHandler<ProcessOrder>,
        IMessageHandler<SubmitCart>,
        IMessageHandler<Test>
    {
    }

    /// <summary>
    /// Business handler for handling ordering fulfillment commands
    /// </summary>
    public class FulfillmentCmdHandler : IFulfillmentCmdHandler
    {
        private IFulfillmentRepository _repository;
        public FulfillmentCmdHandler(IFulfillmentRepository repository)
        {
            _repository = repository;
        }

        public void handle(ProcessOrder command)
        {
            if (!command.OrderId.HasValue)
                throw new Exception("Order ID not provided, cannot process order.");

            _repository.ProcessOrder(command.OrderId.Value);
        }

        public void handle(SubmitCart command)
        {
            List<OrderItem> items = new List<OrderItem>();
            foreach (var prop in command.Cart.Products)
            {
                items.Add(new OrderItem()
                {
                    ProductId = prop.Key,
                    Quantity = prop.Value
                });
            }

            var order = new Order()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                Items = items,
                UserId = Guid.Empty,
                Status = OrderStatus.Pending
            };

            _repository.AddOrder(order);
        }

        public void handle(Test message)
        {
            Console.WriteLine("Handled test message!");
        }
    }
}
