using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LinePayEC.Models;
using LinePayEC.Models.RequestModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LinePayEC.WebApi.DotNetCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LinePayController : ControllerBase
    {
        private readonly string _baseAddress;
        private readonly string _channelSecret;
        private readonly string _channelId;
        
        public LinePayController()
        {
            this._baseAddress = "https://sandbox-api-pay.line.me";
            this._channelSecret = "6a8a58e1541585215be8a7fdfda7d24e";
            this._channelId = "1654864310";
        }

        [Route("reserve")]
        public async Task<IActionResult> Reserve()
        {
            var requestApi = GetReserveData();
            var nonce = Guid.NewGuid().ToString();
            var reserveUrl = "/v3/payments/request";

            var client = new LinePayClient(_baseAddress);
            var requestJson = JsonConvert.SerializeObject(requestApi, client.SerializerSettings);
            var signature = client.GetSignature((_channelSecret + reserveUrl + requestJson + nonce), _channelSecret);

            var result = await client.ReserveAsync(requestApi, _channelId, nonce, signature);

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
