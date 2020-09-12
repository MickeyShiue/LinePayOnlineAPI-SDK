using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LinePayEC.Models.RequestModels;
using LinePayEC.Models.ResponseModels.CaptureResponse;
using LinePayEC.Models.ResponseModels.CheckPaymentStatusResponse;
using LinePayEC.Models.ResponseModels.ConfirmResponse;
using LinePayEC.Models.ResponseModels.PaymentDetailsResponse;
using LinePayEC.Models.ResponseModels.RefundResponse;
using LinePayEC.Models.ResponseModels.ReserveResponse;
using LinePayEC.Models.ResponseModels.VoidResponse;
using Newtonsoft.Json;

namespace LinePayEC
{
    public class LinePayClient
    {
        private readonly HttpClient _client;
        private readonly string _version;
        public readonly JsonSerializerSettings SerializerSettings;

        public LinePayClient(string baseAddress, string version = "v3")
        {
            _version = version;
            _client = new HttpClient { BaseAddress = new Uri(baseAddress) };
            SerializerSettings = new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore };
        }

        /// <summary>
        /// 請求付款
        /// </summary>
        /// <param name="reserve"></param>
        /// <param name="channelId"></param>
        /// <param name="uuid"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public async Task<ReserveResponse> ReserveAsync(Reserve reserve, string channelId, string uuid, string signature)
        {
            this.SetHttpHeader(channelId, uuid, signature);
            return await this.Post<Reserve, ReserveResponse>(reserve, $"/{_version}/payments/request");


            //HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"/{_version}/payments/request")
            //{
            //    Content = new StringContent(JsonConvert.SerializeObject(reserve, SerializerSettings), Encoding.UTF8, "application/json")
            //};

            //var response = await _client.SendAsync(httpRequestMessage);

            //if (response.IsSuccessStatusCode)
            //    return JsonConvert.DeserializeObject<ReserveResponse>(await response.Content.ReadAsStringAsync());
            //else
            //    throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// 確認付款
        /// </summary>
        /// <param name="confirm"></param>
        /// <param name="channelId"></param>
        /// <param name="uuid"></param>
        /// <param name="signature"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<ConfirmResponse> ConfirmAsync(Confirm confirm, string channelId, string uuid, string signature, long transactionId)
        {
            this.SetHttpHeader(channelId, uuid, signature);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"/{_version}/payments/{transactionId}/confirm")
            {
                Content = new StringContent(JsonConvert.SerializeObject(confirm, SerializerSettings), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ConfirmResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// Capture API，手動請款，進行後續請款處理，才能完成所有付款流程。
        /// </summary>
        /// <param name="capture"></param>
        /// <param name="channelId"></param>
        /// <param name="uuid"></param>
        /// <param name="signature"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<CaptureResponse> CaptureAsync(Capture capture, string channelId, string uuid, string signature, long transactionId)
        {
            this.SetHttpHeader(channelId, uuid, signature);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"/{_version}/payments/authorizations/{transactionId}/capture")
            {
                Content = new StringContent(JsonConvert.SerializeObject(capture, SerializerSettings), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<CaptureResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// 取消授權 Void API，僅對已授權的交易產生影響，如是已請款的交易，需使用Refund API進行退款
        /// </summary>
        /// <param name="channelId"></param>
        /// <param name="uuid"></param>
        /// <param name="signature"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<VoidResponse> VoidAsync(string channelId, string uuid, string signature, long transactionId)
        {
            this.SetHttpHeader(channelId, uuid, signature);

            var response = await _client.PostAsync($"/{_version}/payments/authorizations/{transactionId}/void", null);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<VoidResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// (退款)，本 API 用以取消已付款(購買完成)的交易，並可支援部分退款。呼叫時需要帶入該筆付款的 LINE Pay 原始交易序號(transactionId)
        /// </summary>
        /// <param name="refund"></param>
        /// <param name="channelId"></param>
        /// <param name="uuid"></param>
        /// <param name="signature"></param>
        /// <param name="transactionId"></param>
        /// <returns></returns>
        public async Task<RefundResponse> RefundAsync(Refund refund, string channelId, string uuid, string signature, long transactionId)
        {
            this.SetHttpHeader(channelId, uuid, signature);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"/{_version}/payments/{transactionId}/refund")
            {
                Content = new StringContent(JsonConvert.SerializeObject(refund, SerializerSettings), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<RefundResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        /// <summary>
        /// 查詢LINE Pay中的交易記錄，您可以查詢授權和購買完成狀態的交易。使用"fields"設定，可以按交易或訂單資訊，選擇查出交易記錄。
        /// </summary>
        /// <param name="payment"></param>
        /// <param name="channelId"></param>
        /// <param name="uuid"></param>
        /// <param name="signature"></param>
        /// <returns></returns>
        public async Task<PaymentDetailsResponse> PaymentDetailsAsync(PaymentDetails payment, string channelId, string uuid, string signature)
        {
            this.SetHttpHeader(channelId, uuid, signature);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"/{_version}/payments?transactionId={payment.TransactionId}&orderId={payment.OrderId}");

            var response = await _client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<PaymentDetailsResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }

        public async Task<CheckPaymentStatusResponse> CheckAsync(string channelId, string uuid, string signature, long transactionId)
        {
            this.SetHttpHeader(channelId, uuid, signature);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, $"/{_version}/payments/requests/{transactionId}/check");

            var response = await _client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<CheckPaymentStatusResponse>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }


        public string GetSignature(string message, string key)
        {
            return LinePayClientHelper.Encrypt(message, key);
        }

        /// <summary>
        /// 設定 Request HttpHeader
        /// </summary>
        /// <param name="channelId">金流整合資訊 - Channel ID</param>
        /// <param name="uuid">UUID or timestamp(時間戳)</param>
        /// <param name="signature">HMAC Base64 簽章</param>
        private void SetHttpHeader(string channelId, string uuid, string signature)
        {
            LinePayClientHelper.HttpHeaderValueValid(channelId, uuid, signature);

            _client.DefaultRequestHeaders.Clear();
            _client.DefaultRequestHeaders.TryAddWithoutValidation("X-LINE-ChannelId", channelId);
            _client.DefaultRequestHeaders.TryAddWithoutValidation("X-LINE-Authorization-Nonce", uuid);
            _client.DefaultRequestHeaders.TryAddWithoutValidation("X-LINE-Authorization", signature);
            _client.DefaultRequestHeaders.TryAddWithoutValidation("Content-Type", "application/json");
        }

        private async Task<TResult> Post<TEntity, TResult>(TEntity entity, string requestUri)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, requestUri)
            {
                Content = new StringContent(JsonConvert.SerializeObject(entity, SerializerSettings), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<TResult>(await response.Content.ReadAsStringAsync());
            else
                throw new Exception(await response.Content.ReadAsStringAsync());
        }
    }
}
