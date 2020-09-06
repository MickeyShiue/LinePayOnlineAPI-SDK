using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LinePayEC.Models.RequestModels;
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
            SerializerSettings = new JsonSerializerSettings() { DefaultValueHandling = DefaultValueHandling.Ignore };
        }

        public async Task<object> ReserveAsync(Reserve reserve, string channelId, string uuid, string signature)
        {
            this.SetHttpHeader(channelId, uuid, signature);

            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"/{_version}/payments/request")
            {
                Content = new StringContent(JsonConvert.SerializeObject(reserve, SerializerSettings), Encoding.UTF8, "application/json")
            };

            var response = await _client.SendAsync(httpRequestMessage);

            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<object>(await response.Content.ReadAsStringAsync());
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
    }
}
