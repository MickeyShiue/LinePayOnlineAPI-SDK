using Newtonsoft.Json;

namespace LinePayEC.Models.RequestModels
{
    public class Confirm
    {
        /// <summary>
        /// 付款金額
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// 貨幣(ISO 4217)
        ///  支援下列貨幣：
        /// USD
        /// JPY
        /// TWD
        /// THB
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
