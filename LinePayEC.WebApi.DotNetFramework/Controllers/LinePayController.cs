using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web.Http;
using LinePayEC.Models;
using LinePayEC.Models.RequestModels;
using Newtonsoft.Json;

namespace LinePayEC.WebApi.DotNetFramework.Controllers
{
  
    public class LinePayController : ApiController
    {
        private readonly string _baseAddress;
        private readonly string _channelSecret;
        private readonly string _channelId;

        public LinePayController()
        {
            this._baseAddress = "https://api-pay.line.me";
            this._channelSecret = "6a8a58e1541585215be8a7fdfda7d24e";
            this._channelId = "1654864310";
        }

        [HttpGet]
        public async Task<IHttpActionResult> Reserve()
        {
            var requestApi = GetReserveData();
            var nonce = Guid.NewGuid().ToString();
            var reserveUrl = "/v3/payments/request";

            var client = new LinePayClient(_baseAddress, _channelId);
            var requestJson = JsonConvert.SerializeObject(requestApi, client.SerializerSettings);
            var signature = client.GetSignature((_channelSecret + reserveUrl + requestJson + nonce), _channelSecret);

            var result = await client.ReserveAsync(requestApi, nonce, signature);

            return Json(result);
        }

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
                    Name = "拉拉雄",
                    Quantity = 2,
                    Price = 50
                }
            };

            reserve.Packages.Add
            (
                new Packages { Id = Guid.NewGuid().ToString(), Amount = 100, Products = products }
            );

            reserve.RedirectUrls.ConfirmUrl = "https://tw.yahoo.com/V1";
            reserve.RedirectUrls.CancelUrl = "https://tw.yahoo.com/V2";

            return reserve;
        }

    }
}
