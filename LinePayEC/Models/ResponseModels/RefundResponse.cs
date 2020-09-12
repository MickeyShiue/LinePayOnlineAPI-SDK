using Newtonsoft.Json;

namespace LinePayEC.Models.ResponseModels.RefundResponse
{
    public class RefundResponse
    {
        /// <summary>
        /// 結果代碼
        /// 0000 成功
        /// 1101 買家不是LINE Pay的用戶
        /// 1102 買方被停止交易
        /// 1104 此商家不存在
        /// 1105 此商家無法使用LINE Pay
        /// 1106 標頭(Header)資訊錯誤
        /// 1124 金額有誤（scale）
        /// 1150 交易記錄不存在
        /// 1155 交易編號不符合退款資格
        /// 1163 可退款期限已過無法退款
        /// 1164 退款金額超過限制金額
        /// 1165 已經退款而關閉的交易
        /// 1179 無法處理的狀態
        /// 1198 API呼叫重複
        /// 1199 內部請求錯誤
        /// 1264 LINE Pay一卡通相關錯誤
        /// 9000 內部錯誤
        /// </summary>
        [JsonProperty("returnCode")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 結果訊息或失敗原因
        /// </summary>
        [JsonProperty("returnMessage")]
        public string ReturnMessage { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }

    public class Info
    {
        /// <summary>
        /// 退款序號（該次退款產生的新序號, 19 digits）
        /// </summary>
        [JsonProperty("refundTransactionId")]
        public long RefundTransactionId { get; set; }

        /// <summary>
        /// 退款日期（ISO 8601）
        /// </summary>
        [JsonProperty("refundTransactionDate")]
        public string RefundTransactionDate { get; set; }
    }
}
