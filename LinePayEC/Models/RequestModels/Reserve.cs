using System.Collections.Generic;
using Newtonsoft.Json;

namespace LinePayEC.Models.RequestModels
{
    public class Reserve
    {
        public Reserve()
        {
            this.Packages = new List<Packages>();
            this.RedirectUrls = new RedirectUrls();
        }

        /// <summary>
        /// 付款金額 = sum(packages[].amount) + sum(packages[].userFee) + options.shipping.feeAmount
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// 貨幣（ISO 4217）支援貨幣：USD、JPY、TWD、THB
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// 商家訂單編號商家管理的唯一ID
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// Packages 物件
        /// </summary>
        [JsonProperty("packages")]
        public List<Packages> Packages { get; set; }

        /// <summary>
        /// RedirectUrls 物件
        /// </summary>
        [JsonProperty("redirectUrls")]
        public RedirectUrls RedirectUrls { get; set; }

        /// <summary>
        /// Options 物件
        /// </summary>
        [JsonProperty("options")]
        public Options Options { get; set; }
    }
}
