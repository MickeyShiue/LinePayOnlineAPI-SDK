using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class Address
    {
        /// <summary>
        /// 收貨國家
        /// </summary>
        [JsonProperty("country")]
        public string Country { get; set; }

        /// <summary>
        /// 收貨地郵政編碼
        /// </summary>
        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }

        /// <summary>
        /// 收貨地區
        /// </summary>
        [JsonProperty("state")]
        public string State { get; set; }

        /// <summary>
        /// 收貨省市區
        /// </summary>
        [JsonProperty("city")]
        public string City { get; set; }

        /// <summary>
        /// 收貨地址
        /// </summary>
        [JsonProperty("detail")]
        public string Detail { get; set; }

        /// <summary>
        /// 詳細地址資訊
        /// </summary>
        [JsonProperty("optional")]
        public string Optional { get; set; }

        /// <summary>
        /// Recipient 物件
        /// </summary>
        public Recipient Recipient { get; set; }
    }
}
