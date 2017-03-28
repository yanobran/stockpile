using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using StockPile.Api.config;
using Microsoft.Extensions.Options;

namespace StockPile.Api.Controllers
{
    [Route("api/[controller]")]
    public class InventoryController : Controller
    {
        private readonly StockPileOptions _options;

        public InventoryController(IOptions<StockPileOptions> optionsWrapper)
        {
            _options = optionsWrapper.Value;
        }

        [HttpGet()]
        [Route("products")]
        public string GetProducts(string name = null, string category = null, Guid? brand = null)
        {
            List<string> parameters = new List<string>();

            if (!string.IsNullOrEmpty(name))
                parameters.Add($"name={name}");
            if (!string.IsNullOrEmpty(category))
                parameters.Add($"category={category}");
            if (brand.HasValue)
                parameters.Add($"brand={brand}");

            string path = (parameters.Count() > 0) ? "?" + String.Join("&", parameters) : "";
            path = "/inventory/products" + path;
            
            var client = new HttpClient();

            var stringTask = client.GetStringAsync(_options.ServiceBase + path);
            var msg = stringTask.Result;
            
            return msg;
        }

        [HttpGet()]
        [Route("product/{id}")]
        public string GetProduct(Guid id)
        {
            var client = new HttpClient();
            var stringTask = client.GetStringAsync(_options.ServiceBase + $"/inventory/product?id={id}");

            var msg = stringTask;

            return msg.Result;
        }

        [HttpGet()]
        [Route("brands")]
        public string GetBrands()
        {
            var client = new HttpClient();
            var stringTask = client.GetStringAsync(_options.ServiceBase + $"/inventory/brands");
            
            var msg = stringTask;

            return msg.Result;
        }

        [HttpGet()]
        [Route("categories")]
        public string GetCategories()
        {
            var client = new HttpClient();
            var stringTask = client.GetStringAsync(_options.ServiceBase + $"/inventory/categories");

            var msg = stringTask;

            return msg.Result;
        }
    }
}
