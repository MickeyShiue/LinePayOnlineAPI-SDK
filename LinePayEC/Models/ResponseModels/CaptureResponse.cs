using System.Collections.Generic;
using Newtonsoft.Json;

namespace LinePayEC.Models.ResponseModels.CaptureResponse
{
    public class CaptureResponse
    {
        /// <summary>
        /// 結果代碼
        /// 0000	成功
        /// 1104	此商家不存在
        /// 1105	此商家無法使用LINE Pay
        /// 1106	標頭(Header)資訊錯誤
        /// 1150	交易記錄不存在
        /// 1155	該交易序號有誤
        /// 1170	使用者帳戶的餘額有變動
        /// 1172	該訂單號(orderId)的交易記錄已經存在
        /// 1179	無法處理的狀態
        /// 1183	付款金額不能小於 0
        /// 1184	付款金額比付款申請時候的金額還大
        /// 1198	API重覆呼叫，或者授權更新過程中，呼叫了Capture API（請幾分鐘後重試一下）
        /// 1199	內部請求錯誤
        /// 1264	LINE Pay一卡通相關錯誤
        /// 1280	信用卡付款時候發生了臨時錯誤
        /// 1281	信用卡付款錯誤
        /// 1282	信用卡授權錯誤
        /// 1283	因有異常交易疑慮暫停交易，請洽 LINE Pay 客服確認
        /// 1284	暫時無法以信用卡付款
        /// 1285	信用卡資訊不完整
        /// 1286	信用卡付款資訊不正確
        /// 1287	信用卡已過期
        /// 1288	信用卡的額度不足
        /// 1289	超過信用卡付款金額上限
        /// 1290	超過一次性付款的額度
        /// 1291	此信用卡已被掛失
        /// 1292	此信用卡已被停卡
        /// 1293	信用卡驗證碼(CVN) 無效
        /// 1294	此信用卡已被列入黑名單
        /// 1295	信用卡號無效
        /// 1296	無效的金額
        /// 1298	信用卡付款遭拒絕
        /// 9000	內部錯誤
        /// </summary>
        [JsonProperty("returnCode")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 結果訊息或失敗原因。例如：
        /// 該商家無法交易
        /// 商家認證資訊錯誤
        /// </summary>
        [JsonProperty("returnMessage")]
        public string ReturnMessage { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }

    public class Info
    {
        /// <summary>
        /// 在請求付款時，商家回應的訂單編號
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// 作為請求付款的結果，回應的交易序號（19個字符）
        /// </summary>
        [JsonProperty("transactionId")]
        public long TransactionId { get; set; }

        [JsonProperty("payInfo")]
        public List<PayInfo> PayInfo { get; set; }
    }

    public class PayInfo
    {
        /// <summary>
        /// 付款方式
        /// 信用卡：CREDIT_CARD
        /// 餘額：BALANCE
        /// 折扣：DISCOUNT(發票金額須扣除)
        /// LINE POINTS：POINT(預設不顯示)
        /// </summary>
        [JsonProperty("method")]
        public string Method { get; set; }

        /// <summary>
        /// 付款金額
        /// </summary>
        [JsonProperty("amount")]
        public int Amount { get; set; }
    }
}
