using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StockPile.Messaging.Http
{
    public interface IHttpServiceBus : IMessageBus
    {
        Task Route(HttpContext context);
    }
}
