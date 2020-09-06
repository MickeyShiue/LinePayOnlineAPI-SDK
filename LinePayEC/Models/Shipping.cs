using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class Shipping
    {
        /// <summary>
        /// 收貨地選項
        /// NO_SHIPPING
        /// FIXED_ADDRESS
        /// SHIPPING
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 運費
        /// </summary>
        [JsonProperty("feeAmount")]
        public string FeeAmount { get; set; }

        /// <summary>
        /// 查詢配送方式的URL
        /// </summary>
        [JsonProperty("feeInquiryUrl")]
        public string FeeInquiryUrl { get; set; }

        /// <summary>
        /// 運費查詢類型
        /// CONDITION：收貨地發生變化，就查詢配送方式（運費）
        /// FIXED：作為固定值，收貨地發生變化，也不會查詢配送方式
        /// </summary>
        [JsonProperty("feeInquiryType")]
        public string FeeInquiryType { get; set; }

        /// <summary>
        /// Address 物件
        /// </summary>
        [JsonProperty("address")]
        public Address Address { get; set; }
    }
}
