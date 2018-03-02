using Microsoft.Extensions.Options;
using StockPile.Command.Handlers;
using StockPile.Messaging.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StockPile.Services.ApplicationService.Middleware
{
    public class StockPileServiceBus : HttpServiceBus
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="options">Application config section for configuring message bus routing</param>
        /// <param name="fulfillmentHandler">Handler to handle fulfillment commands</param>
        public StockPileServiceBus(IOptions<HttpServiceBusConfig> options, IFulfillmentCmdHandler fulfillmentHandler)
            : base(options)
        {
            this._handlers.Add(fulfillmentHandler);
        }
    }
}
