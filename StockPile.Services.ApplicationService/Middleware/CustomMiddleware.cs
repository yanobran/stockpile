using Microsoft.AspNetCore.Http;
using StockPile.Query;
using StockPile.Repository;
using StockPile.Query.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace StockPile.Services.ApplicationService
{
    public class CustomMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IInventoryRepository _repository;
        private readonly IInventoryQryHandler _handler;
        public CustomMiddleware(RequestDelegate next, IInventoryRepository repo, IInventoryQryHandler handler)
        {
            _next = next;
            _repository = repo;
            _handler = handler;
        }

        public async Task Invoke(HttpContext context)
        {
            ExecuteQuery(context);
            await _next.Invoke(context);
        }

        private void ExecuteQuery(HttpContext context)
        {
            Console.WriteLine(">>>> Executing Query: " + context.Request.Path.Value);

            if (context.Request.Path.StartsWithSegments("/products"))
            {
                Guid? brand = null;
                Guid temp;
                if(Guid.TryParse(context.Request.Query["brand"], out temp))
                {
                    brand = temp;
                }

                ProductsQry qry = new ProductsQry()
                {
                    Name = context.Request.Query["name"].FirstOrDefault() ?? null,
                    Category = context.Request.Query["category"].FirstOrDefault() ?? null,
                    Brand = brand
                };

                var data = _handler.Handle(qry);

                context.Response.StatusCode = 200;
                context.Response.WriteAsync(JsonConvert.SerializeObject(data));
                
            }
            else if (context.Request.Path.StartsWithSegments("/product"))
            {
                Guid temp;
                if (Guid.TryParse(context.Request.Query["id"], out temp))
                {
                    ProductQry qry = new ProductQry() { Id = temp };
                    var data = _handler.Handle(qry);
                    context.Response.StatusCode = 200;
                    context.Response.WriteAsync(JsonConvert.SerializeObject(data));
                } else
                {
                    context.Response.StatusCode = 404;
                }
            }
            else if (context.Request.Path.StartsWithSegments("/brands"))
            {
                var data = _handler.Handle(new BrandQry());
                context.Response.StatusCode = 200;
                context.Response.WriteAsync(JsonConvert.SerializeObject(data));
            }
            else if (context.Request.Path.StartsWithSegments("/categories"))
            {
                var data = _handler.Handle(new CategoryQry());
                context.Response.StatusCode = 200;
                context.Response.WriteAsync(JsonConvert.SerializeObject(data));
            }
        }
        
    }
}
