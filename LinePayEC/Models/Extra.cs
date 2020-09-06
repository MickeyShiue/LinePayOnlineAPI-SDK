using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class Extra
    {
        /// <summary>
        /// 商店或分店名稱(僅會顯示前 100 字元)
        /// </summary>
        [JsonProperty("branchName")]
        public string BranchName { get; set; }

        /// <summary>
        /// 商店或分店代號，可支援英數字及特殊字元
        /// </summary>
        [JsonProperty("branchId")]
        public string BranchId { get; set; }

        /// <summary>
        /// 點數限制資訊
        /// useLimit: 不可使用點數折抵的金額
        /// rewardLimit: 不可回饋點數的金額
        ///  "promotionRestriction": {
        ///    "useLimit": 100,
        ///    "rewardLimit": 100
        ///  }
        /// </summary>
        [JsonProperty("promotionRestriction")]
        public object PromotionRestriction { get; set; }
    }
}
