using System;
using System.Collections.Generic;
using System.Text;

namespace StockPile.Messaging.Http
{
    public class HttpServiceBusConfig
    {
        public HttpServiceBusConfig() { }

        public Dictionary<string, string> MessageRoutes { get; } = new Dictionary<string, string>();
    }
}
