using System.Collections.Generic;
using Newtonsoft.Json;

namespace LinePayEC.Models.RequestModels
{
    public class PaymentDetails
    {
        /// <summary>
        /// 由LINE Pay建立的交易序號或退款序號
        /// </summary>
        [JsonProperty("transactionId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// 商家訂單編號
        /// </summary>
        [JsonProperty("long")]
        public string OrderId { get; set; }

        /// <summary>
        /// 可以選擇查詢物件
        /// transaction
        /// order
        /// 預設為所有
        /// </summary>
        [JsonProperty("fields")]
        public string Fields { get; set; }
    }
}
