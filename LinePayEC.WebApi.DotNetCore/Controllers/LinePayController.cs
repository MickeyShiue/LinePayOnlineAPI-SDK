using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinePayEC.Models;
using LinePayEC.Models.RequestModels;
using LinePayEC.WebApi.DotNetCore.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace LinePayEC.WebApi.DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinePayController : ControllerBase
    {
        private readonly AppSettings _config;

        public LinePayController(IOptions<AppSettings> config)
        {
            this._config = config.Value;
        }

        [Route("reserve")]
        public async Task<IActionResult> Reserve()
        {
            var requestApi = GetReserveData();
            var nonce = Guid.NewGuid().ToString();
            var reserveUrl = "/v3/payments/request";

            var client = new LinePayClient(_config.BaseAddress);
            var requestJson = JsonConverterFacade.SerializeObject(requestApi, client.SerializerSettings);
            var signature = client.GetSignature((_config.ChannelSecret + reserveUrl + requestJson + nonce), _config.ChannelSecret);

            var result = await client.ReserveAsync(requestApi, _config.ChannelId, nonce, signature);

            return Ok(result);
        }


        #region mocking Data

        private static Reserve GetReserveData()
        {
            Reserve reserve = new Reserve
            {
                Amount = 100,
                Currency = "TWD",
                OrderId = Guid.NewGuid().ToString()
            };


            List<Products> products = new List<Products>()
            {
               new Products()
               {
                   Id = Guid.NewGuid().ToString(),
                   Name = "銅鑼燒",
                   Quantity = 2,
                   Price = 50
               }
            };

            reserve.Packages.Add
            (
                new Packages { Id = Guid.NewGuid().ToString(), Amount = 100, Products = products }
            );

            reserve.RedirectUrls.ConfirmUrl = "https://YouDomain/ConfirmUrl";
            reserve.RedirectUrls.CancelUrl = "https://YouDomain/CancelUrl";

            return reserve;
        }
        #endregion
    }
}
