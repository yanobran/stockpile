using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace StockPile.Messaging.Http
{
    public class HttpServiceBusRouter
    {
        private readonly RequestDelegate _next;
        private Dictionary<string, Type> _routes = new Dictionary<string, Type>();
        protected IHttpServiceBus _bus;

        /// <summary>
        /// Initializes the routing dictionary which maps URL subpaths to commands. 
        /// </summary>
        /// <param name="next">The next RequestDelegate in the HTTP Middleware pipeline</param>
        /// <param name="options">
        /// Service Bus Configuration containing command routing Dictionary with key/value = string/type = route/"command, assembly"
        /// For example, "process": "StockPile.Command.ProcessOrder, StockPile.Command"
        /// </param>
        /// <param name="bus">IHttpServiceBus Service Bus implementation</param>
        public HttpServiceBusRouter(RequestDelegate next, IOptions<HttpServiceBusConfig> options, IHttpServiceBus bus)
        {
            try
            {
                _bus = bus;

                foreach (KeyValuePair<string, string> kvp in options.Value.MessageRoutes)
                {
                    Type msgType = Type.GetType(kvp.Value);
                    var route = kvp.Key;
                    route = route.StartsWith("/") ? route : $"/{route}";

                    if (msgType == null)
                        throw new Exception("HttpServiceBus routing message type is not a valid class. Please ensure that the correct .dll is supplied.");

                    _routes.Add(route, msgType);
                }
            }
            catch (Exception e)
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
                var msgType = _routes[path];

                //Route / Type mapping found, deserialize command and send to message handler
                if (msgType != null)
                {
                    using (var rdr = new StreamReader(context.Request.Body))
                    {
                        var body = await rdr.ReadToEndAsync();
                        var msg = JsonConvert.DeserializeObject(body, msgType);

                        if (msg == null)
                            throw new Exception("Cannot route message, does not match type mapped in route mappings");
                        else
                            _bus.Send(msg);
                    }
                }
            }
        }
    }
}
