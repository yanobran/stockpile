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
using Microsoft.AspNetCore.Hosting.Server.Features;
using StockPile.Data.MemoryDbImpl.bootstrap;
using StockPile.Messaging.Http;

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

            services.AddSingleton<IStockPileRepository, InventoryMemRepo>();

            services.Configure<MemoryDbConfig>(Configuration.GetSection("MemoryDb"));
            //services.Configure<HttpServiceBusConfig>(Configuration.GetSection("HttpServiceBus"));

            services.AddTransient<IInventoryQryHandler, InventoryQryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            Console.WriteLine(">>>> Configuring Application");
            Console.WriteLine($">>>> WebRootPath: {env.WebRootPath}");
            
            var svrfeatures = (Microsoft.AspNetCore.Http.Features.FeatureCollection)app.Properties["server.Features"];
            
            foreach(KeyValuePair<Type, object> feature in svrfeatures)
            {
                var addrFeature = (IServerAddressesFeature)feature.Value;
                var addresses = addrFeature?.Addresses;
                Console.WriteLine(">>>> Host Addresses:");

                foreach(var a in addresses)
                {
                    Console.WriteLine($"\t--> Address: {a}");
                }
            }

            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.Map("/inject", appBuilder =>
            //{
            //    appBuilder.UseMiddleware<HttpServiceBus>();
            //    appBuilder.Run(async (context) => { });
            //});

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
