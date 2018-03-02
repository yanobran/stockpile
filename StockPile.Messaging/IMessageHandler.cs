using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Messaging
{
    public interface IMessageHandler<T>
    {
        void handle(T message);
    }
}
