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
            var reserveData = GetReserveData();
            var nonce = Guid.NewGuid().ToString();
            var reserveUrl = "/v3/payments/request";

            var client = new LinePayClient(_config.BaseAddress);
            var requestJson = JsonConverterFacade.SerializeObject(reserveData, client.SerializerSettings);
            var signature = client.GetSignature((_config.ChannelSecret + reserveUrl + requestJson + nonce), _config.ChannelSecret);

            var result = await client.ReserveAsync(reserveData, _config.ChannelId, nonce, signature);

            return Ok(result);
        }

        [Route("confirm")]
        public async Task<IActionResult> Confirm(long transactionId, Guid orderId)
        {
            var confirmData = GetConfirmData();
            var nonce = Guid.NewGuid().ToString();
            var confirmUrl = $"/v3/payments/{transactionId}/confirm";

            var client = new LinePayClient(_config.BaseAddress);
            var requestJson = JsonConverterFacade.SerializeObject(confirmData, client.SerializerSettings);
            var signature = client.GetSignature((_config.ChannelSecret + confirmUrl + requestJson + nonce), _config.ChannelSecret);

            var result = await client.ConfirmAsync(confirmData, _config.ChannelId, nonce, signature, transactionId);
            //若不是自動請款請呼叫 client.CaptureAsync()，如果使用非自動請款，請聯繫 LINE PAY API 相關窗口申請


            return Ok(result);
        }

        [Route("void")]
        public async Task<IActionResult> Void(long transactionId)
        {
            var nonce = Guid.NewGuid().ToString();
            var voidUrl = $" /v3/payments/authorizations/{transactionId}/void";

            var client = new LinePayClient(_config.BaseAddress);
            var signature = client.GetSignature((_config.ChannelSecret + voidUrl + null + nonce), _config.ChannelSecret);

            var result = await client.VoidAsync(_config.ChannelId, nonce, signature, transactionId); //Error 1106，可能是因為自動付款沒辦法用

            return Ok(result);
        }

        [Route("Refund")]
        public async Task<IActionResult> Refund(long transactionId)
        {
            var refundData = GetRefund();
            var nonce = Guid.NewGuid().ToString();
            var refundUrl = $"/v3/payments/{transactionId}/refund";

            var client = new LinePayClient(_config.BaseAddress);
            var requestJson = JsonConverterFacade.SerializeObject(refundData, client.SerializerSettings);
            var signature = client.GetSignature((_config.ChannelSecret + refundUrl + requestJson + nonce), _config.ChannelSecret);

            var result = await client.RefundAsync(refundData, _config.ChannelId, nonce, signature, transactionId);

            return Ok(result);
        }

        [Route("Payments")]
        public async Task<IActionResult> Payments()
        {
            var paymentDetails = GetPaymentDetails();
            var nonce = Guid.NewGuid().ToString();
            var paymentUrl = $"/v3/payments";

            var client = new LinePayClient(_config.BaseAddress);
            var queryString = $"transactionId={paymentDetails.TransactionId}&orderId={paymentDetails.OrderId}";
            var signature = client.GetSignature((_config.ChannelSecret + paymentUrl + queryString + nonce), _config.ChannelSecret);

            var result = await client.PaymentDetailsAsync(paymentDetails, _config.ChannelId, nonce, signature);

            return Ok(result);
        }

        [Route("CheckPaymentStatus")]
        public async Task<IActionResult> CheckPaymentStatus()
        {
            long transactionId = 2020091200628346110;
            var nonce = Guid.NewGuid().ToString();
            var checkUrl = $"/v3/payments/requests/{transactionId}/check";

            var client = new LinePayClient(_config.BaseAddress);
            var signature = client.GetSignature((_config.ChannelSecret + checkUrl + nonce), _config.ChannelSecret);
            var result = await client.CheckAsync(_config.ChannelId, nonce, signature, transactionId);

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

            //reserve.RedirectUrls.ConfirmUrl = "https://localhost:44325/api/LinePay/confirm";

            reserve.RedirectUrls.ConfirmUrl = "https://aaaa.com/api/LinePay/confirm";
            reserve.RedirectUrls.CancelUrl = "https://localhost:44325/api/LinePay/cancel";

            return reserve;
        }

        private static Confirm GetConfirmData()
        {
            return new Confirm()
            {
                Amount = 100,
                Currency = "TWD"
            };
        }

        private static Capture GetCapture()
        {
            return new Capture
            {
                Amount = 100,
                Currency = "TWD"
            };
        }

        private static Refund GetRefund()
        {
            return new Refund
            {
                RefundAmount = 100
            };
        }

        private static PaymentDetails GetPaymentDetails()
        {
            // 正常訂單
            return new PaymentDetails
            {
                TransactionId = 2020091200628341710,
                OrderId = "614def51-9a0b-4570-9688-c07080df31fd"
            };

            // 退款訂單
            //return new PaymentDetails
            //{
            //    // 2020091200628341111
            //    TransactionId = 2020091200628341111,
            //    OrderId = "6eaa286c-9803-4fdf-8ab5-e2ef5cdb3a9d"
            //};
        }

        #endregion
    }
}
