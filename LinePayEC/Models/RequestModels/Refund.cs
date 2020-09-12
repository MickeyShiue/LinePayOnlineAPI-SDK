using Newtonsoft.Json;

namespace LinePayEC.Models.RequestModels
{
    public class Refund
    {
        /// <summary>
        /// 返回空值的話，進行全部退款
        /// </summary>
        [JsonProperty("refundAmount")]
        public int RefundAmount { get; set; }

        /// <summary>
        /// 點數限制資訊
        /// useLimit: 不可使用點數折抵的金額
        /// rewardLimit: 不可回饋點數的金額
        /// "promotionRestriction": {
        ///    "useLimit": 100,
        ///    "rewardLimit": 100
        /// }
        /// </summary>
        [JsonProperty("options")]
        public Options Options { get; set; }
    }
}
