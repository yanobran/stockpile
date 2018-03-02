using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Reflection;
using Newtonsoft.Json;
using System.IO;

namespace StockPile.Messaging.Http
{
    /// <summary>
    /// NOTE - This class follows the middleware pattern for .NET Core with the RequestDelegate in the constructor
    ///        and implementing the Task Invoke(HttpContext) method.
    ///      - This module should eventually be moved into it's own assembly/project as the HTTP implementation (StockPile.Messaging.Http)
    ///      - Similarily, should build out StockPile.Messaging.AMQP.*** for AMQP implementations (rabbit/windows service bus/etc.)
    /// </summary>
    public class HttpServiceBus : IHttpServiceBus
    {
        private Dictionary<string, Type> _routes = new Dictionary<string, Type>();
        protected List<object> _handlers = new List<object>();

        /// <summary>
        /// Initializes the route-to-type mapping for dispatching domain commands/events/queries
        /// 
        /// TODO:
        /// It might be a good idea to wrap the routing and handler dictionaries in a class and inject the wrapper 
        /// with the appropriate lifetime configured in the aspnetcore Startup.cs (i.e. transient/singleton/scope)
        /// 
        /// </summary>
        /// <example> 
        /// Message Route Schema: "[route]": "[Name], [Assembly]"
        /// 
        /// "MessageRoutes": {
        ///     "process": "StockPile.Command.ProcessOrder, StockPile.Command",
        ///     "submit": "StockPile.Command.SubmitCart, StockPile.Command",
        /// }
        /// </example>
        /// <param name="options">Uses MS IOptions construct for injecting HttpServiceBusConfig options from AppSettings</param>
        public HttpServiceBus(IOptions<HttpServiceBusConfig> options)
        {
            try
            {
                foreach (KeyValuePair<string, string> kvp in options.Value.MessageRoutes)
                {
                    var route = kvp.Key;
                    Type msgType = Type.GetType(kvp.Value);

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

        public async Task Route(HttpContext context)
        {
            var path = context.Request.Path.Value;
            
            Console.WriteLine($"Path: {context.Request.Path.Value}\t\tType: {_routes[path].FullName}");

            if (_routes.ContainsKey(path))
            {
                var msgType = _routes[path];
                //Route / Type mapping found, send message to handler
                if (msgType != null)
                {
                    using (var rdr = new StreamReader(context.Request.Body))
                    {
                        var body = await rdr.ReadToEndAsync();
                        var msg = JsonConvert.DeserializeObject(body, msgType);
                        if (msg == null)
                            throw new Exception("Cannot route message, does not match type mapped in route mappings");
                        else
                            this.Send(msg);
                    }
                }
            }
        }

        /// <summary>
        /// This method loops through the registered handlers and determines if one of them handles the message of type T.
        /// If found, the handle method is invoked and the method terminates, otherwise it throws an exception that the Handler
        /// for the given message type is not registered with the bus.
        /// </summary>
        /// <typeparam name="T">This generic parameter represents the type of message being passed into the Send method</typeparam>
        /// <param name="message">The message to be handled by one of the registered handlers</param>
        public void Send<T>(T message)
        {
            MethodInfo meth = null;

            foreach (var handler in this._handlers)
            {
                meth = handler.GetType().GetMethod("handle", new[] { message.GetType() });
                if (meth != null)
                {
                    meth.Invoke(handler, new object[] { message });
                    break;
                }
            }

            if(meth == null)
            {
                throw new Exception($"Handler not registered for message type {message.GetType().Name}");
            }
        }
    }
}
