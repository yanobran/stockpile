using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Command.Handlers
{
    public interface ICommandHandler<T> where T : ICommand
    {
        void handle(T command);
    }
}
