using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using StockPile.Repository;
using StockPile.Data.MemoryDbImpl;
using StockPile.Query.Handlers;
using Microsoft.Extensions.Configuration;
using StockPile.Messaging.Http;
using StockPile.Command.Handlers;
using StockPile.Services.ApplicationService.Middleware;

namespace StockPile.Services.ApplicationService
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();

            // App Settings
            services.Configure<MemoryDbConfig>(Configuration.GetSection("MemoryDb"));
            services.Configure<HttpServiceBusConfig>(Configuration.GetSection("HttpServiceBus"));

            // Singletons
            services.AddSingleton<IInventoryRepository, InventoryMemoryRepo>();
            services.AddSingleton<IFulfillmentRepository, FulfillmentMemoryRepo>();
            services.AddSingleton<IHttpServiceBus, StockPileServiceBus>();

            // Handlers
            services.AddTransient<IInventoryQryHandler, InventoryQryHandler>();
            services.AddTransient<IFulfillmentCmdHandler, FulfillmentCmdHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Map("/inject", appBuilder =>
            {
                //appBuilder.UseMiddleware<ServiceBusMiddleware>();
                appBuilder.UseMiddleware<HttpServiceBusRouter>();
                appBuilder.Run(async (context) => { });
            });

            app.Map("/inventory", appBuilder =>
            {
                appBuilder.UseMiddleware<CustomMiddleware>();
                appBuilder.Run(async (context) => { });
            });
            
            app.Map("/fulfillment", appBuilder =>
            {
                
            });

            app.Run(async (context) =>
            {
                context.Response.StatusCode = 400;
                await context.Response.WriteAsync("");
            });
        }
    }
}
