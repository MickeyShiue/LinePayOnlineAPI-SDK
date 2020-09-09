using Newtonsoft.Json;

namespace LinePayEC.Models.RequestModels
{
    public class Capture
    {
        /// <summary>
        /// 付款金額
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// 貨幣（ISO 4217）
        /// 支援下列貨幣:
        /// USD
        /// JPY
        /// TWD
        /// THB
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// 點數限制資訊
        /// </summary>
        [JsonProperty("options")]
        public Options Options { get; set; }
    }
}
