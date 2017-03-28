using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Messaging
{
    public interface IMessageHandler
    {
        void HandleMessage<T>(T message) where T : IMessage;
    }
}
