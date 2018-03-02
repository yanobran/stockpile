using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using StockPile.Api.config;
using Microsoft.Extensions.Options;
using StockPile.Model.Fulfillment;
using Newtonsoft.Json;
using System.Text;
using StockPile.Command;

namespace StockPile.Api.Controllers
{
    [Route("api/[controller]")]
    public class FulfillmentController : Controller
    {
        private readonly StockApiPileOptions _options;

        public FulfillmentController(IOptions<StockApiPileOptions> optionsWrapper)
        {
            _options = optionsWrapper.Value;
        }

        [HttpPost()]
        [Route("submit")]
        public IActionResult SubmitCart([FromBody] ShoppingCart cart)
        {
            using (var client = new HttpClient())
            {
                SubmitCart cmd = new SubmitCart()
                {
                    Cart = cart
                };

                var response = client.PostAsync(_options.ServiceBase + "/inject/submit",
                    new StringContent(JsonConvert.SerializeObject(cmd).ToString(),
                        Encoding.UTF8, "application/json"))
                        .Result;

                return Json("success");
            }
        }
    }
}
