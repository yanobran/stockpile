using System;

namespace StockPile.Messaging
{
    public interface IMessageBus
    {
        void Send<T>(T message);
    }
}
