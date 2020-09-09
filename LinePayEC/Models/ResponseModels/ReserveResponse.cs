using Newtonsoft.Json;

namespace LinePayEC.Models.ResponseModels.ReserveResponse
{
    public class ReserveResponse
    {
        /// <summary>
        ///  結果代碼
        ///  0000	成功
        ///  1104	此商家不存在
        ///  1105	此商家無法使用LINE Pay
        ///  1106	標頭(Header)資訊錯誤
        ///  1124	金額有誤（scale）
        ///  1145	正在進行付款
        ///  1172	該訂單編號(orderId)的交易記錄已經存在
        ///  1178	商家不支援該貨幣
        ///  1183	付款金額不能小於 0
        ///  1194	此商家無法使用自動付款
        ///  2101	參數錯誤
        ///  2102	JSON資料格式錯誤
        ///  9000	內部錯誤
        /// </summary>
        [JsonProperty("returnCode")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 結果訊息
        /// </summary>
        [JsonProperty("returnMessage")]
        public string ReturnMessage { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }

    public class PaymentUrl
    {
        /// <summary>
        /// 用來跳轉到付款頁的Web URL
        /// 在網頁請求付款時使用
        /// 在跳轉到LINE Pay等待付款頁時使用
        /// 不經參數，直接跳轉到傳來的URL
        /// 在Desktop版，彈窗大小為Width：700px，Height：546px
        /// </summary>
        [JsonProperty("web")]
        public string Web { get; set; }

        /// <summary>
        /// 用來跳轉到付款頁的App URL
        /// 在應用程式發起付款請求時使用
        /// 在從商家應用跳轉到LINE Pay時使用
        /// </summary>
        [JsonProperty("app")]
        public string App { get; set; }
    }

    public class Info
    {
        [JsonProperty("paymentUrl")]
        public PaymentUrl PaymentUrl { get; set; }

        /// <summary>
        /// 交易序號
        /// </summary>
        [JsonProperty("transactionId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// 該代碼在LINE Pay可以代替掃描器使用
        /// </summary>
        [JsonProperty("paymentAccessToken")]
        public string PaymentAccessToken { get; set; }
    }
}
