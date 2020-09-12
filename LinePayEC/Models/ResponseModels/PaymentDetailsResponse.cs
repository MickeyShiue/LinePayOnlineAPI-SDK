using System.Collections.Generic;
using Newtonsoft.Json;

namespace LinePayEC.Models.ResponseModels.PaymentDetailsResponse
{
    public class PaymentDetailsResponse
    {
        /// <summary>
        /// 結果代碼
        /// 0000	成功
        /// 1104	此商家不存在
        /// 1105	此商家無法使用 LINE Pay
        /// 1106	標頭(Header)資訊錯誤
        /// 1150	交易記錄不存在
        /// 1177	超過允許查詢的交易數目(100筆)
        /// 9000	內部錯誤
        /// </summary>
        [JsonProperty("returnCode")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 結果訊息或失敗原因
        /// </summary>
        [JsonProperty("returnMessage")]
        public string ReturnMessage { get; set; }

        [JsonProperty("info")]
        public List<Info> Info { get; set; }
    }

    public class PayInfo
    {
        /// <summary>
        /// 付款方式
        /// 信用卡：CREDIT_CARD
        /// 餘額：BALANCE
        /// 折扣：DISCOUNT(發票金額須扣除)
        ///LINE POINTS：POINT(預設不顯示)
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// 交易金額（建立交易序號時提供的金額）在檢視原始交易時，最終交易金額的演算法如下：sum(info[].payInfo[].amount) – sum(refundList[].refundAmount)
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }
    }

    public class RefundList
    {
        /// <summary>
        /// 退款序號（19個字元）
        /// </summary>
        [JsonProperty("refundTransactionId")]
        public object RefundTransactionId { get; set; }

        /// <summary>
        /// 交易分類
        /// PAYMENT_REFUND：退款
        /// PARTIAL_REFUND：部分退款
        /// </summary>
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }

        /// <summary>
        /// 退款
        /// </summary>
        [JsonProperty("refundAmount")]
        public int RefundAmount { get; set; }

        /// <summary>
        /// 退款日期 (ISO-8601)
        /// </summary>
        [JsonProperty("refundTransactionDate")]
        public string RefundTransactionDate { get; set; }
    }

    public class Info
    {
        /// <summary>
        /// 交易序號（19個字元）
        /// </summary>
        [JsonProperty("transactionId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// 交易日期（ISO-8601）
        /// </summary>
        [JsonProperty("transactionDate")]
        public string TransactionDate { get; set; }

        /// <summary>
        /// 交易分類
        /// PAYMENT：付款
        /// PAYMENT_REFUND：退款
        /// PARTIAL_REFUND：部分退款
        /// </summary>
        [JsonProperty("transactionType")]
        public string TransactionType { get; set; }

        /// <summary>
        /// 退款金額
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }

        /// <summary>
        /// 商品名稱
        /// </summary>
        [JsonProperty("productName")]
        public string ProductName { get; set; }

        /// <summary>
        /// 貨幣（ISO 4217）
        /// </summary>
        [JsonProperty("currency")]
        public string Currency { get; set; }

        /// <summary>
        /// 商家訂單編號
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// 原交易序號
        /// </summary>
        [JsonProperty("originalTransactionId")]
        public long OriginalTransactionId { get; set; }


        [JsonProperty("payInfo")]
        public List<PayInfo> PayInfo { get; set; }

        [JsonProperty("refundList")]
        public List<RefundList> RefundList { get; set; }

        [JsonProperty("packages")]
        public List<Packages> Packages { get; set; }

        [JsonProperty("shipping")]
        public Shipping Shipping { get; set; }
    }
}
