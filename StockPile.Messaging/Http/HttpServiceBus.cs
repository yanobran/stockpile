using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
//using StockPile.Command.Handlers;

namespace StockPile.Messaging.Http
{
    /// <summary>
    /// NOTE - This class follows the middleware pattern for .NET Core with the RequestDelegate in the constructor
    ///        and implementing the Task Invoke(HttpContext) method.
    ///      - This module should eventually be moved into it's own assembly/project as the HTTP implementation (StockPile.Messaging.Http)
    ///      - Similarily, should build out StockPile.Messaging.AMQP.*** for AMQP implementations (rabbit/windows service bus/etc.)
    /// </summary>
    public class HttpServiceBus : IMessageBus
    {
        private readonly RequestDelegate _next;
        private Dictionary<string, Type> _routes = new Dictionary<string, Type>();
        protected List<IMessageHandler> _handlers = new List<IMessageHandler>();
        
        /// <summary>
        /// Initializes the route-to-type mapping for dispatching domain commands/events/queries
        /// </summary>
        public HttpServiceBus(RequestDelegate next, IOptions<HttpServiceBusConfig> options)
        {
            try
            {
                _next = next;
                var config = options.Value;

                foreach (KeyValuePair<string, string> kvp in options.Value.MessageRoutes)
                {
                    var route = kvp.Key;
                    Type msgType = Type.GetType(kvp.Value);

                    _routes.Add(route, msgType);
                }
            } catch(Exception e)
            {
                throw e;
            }
        }

        public async Task Invoke(HttpContext context)
        {
            var path = context.Request.Path.Value;
            
            Console.WriteLine($"Path: {context.Request.Path.Value}\t\tType: {_routes[path].FullName}");

            if (_routes.ContainsKey(path))
            {
                var t = _routes[path];

                foreach (var handler in this._handlers)
                {
                    Type temp = handler.GetType();

                    
                    //var handler = (ICommandHandler<>)

                    // Dynamically invoke handler with injected type.
                }
            }              

            await _next.Invoke(context);
        }

        public void Send<T>(T Message) where T : IMessage
        {

        }
    }
}
