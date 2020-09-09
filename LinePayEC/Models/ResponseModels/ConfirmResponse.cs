using Newtonsoft.Json;
using System.Collections.Generic;

namespace LinePayEC.Models.ResponseModels.ConfirmResponse
{
    public class ConfirmResponse
    {
        /// <summary>
        /// 結果代碼
        /// 0000	成功
        /// 1101	買家不是LINE Pay用戶
        /// 1102	買方被停止交易
        /// 1104	此商家不存在
        /// 1105	此商家無法使用 LINE Pay
        /// 1106	標頭(Header)資訊錯誤
        /// 1110	無法使用的信用卡
        /// 1124	金額錯誤 (scale)
        /// 1141	付款帳戶狀態錯誤
        /// 1142	Balance餘額不足
        /// 1150	交易記錄不存在
        /// 1152	該transactionId的交易記錄已經存在
        /// 1153	付款request時的金額與申請時的金額不一致
        /// 1159	無付款申請資訊
        /// 1169	用來確認付款的資訊錯誤（請訪問LINE Pay設置付款方式與密碼認證）
        /// 1170	使用者帳戶的餘額有變動
        /// 1172	該訂單編號(orderId)的交易記錄已經存在
        /// 1180	付款時限已過
        /// 1198	API調用重覆
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
        /// 1293	信用卡驗證碼 (CVN) 無效
        /// 1294	此信用卡已被列入黑名單
        /// 1295	信用卡號無效
        /// 1296	無效的金額
        /// 1298	信用卡付款遭拒絕
        /// 9000	內部錯誤
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

    public class Info
    {
        /// <summary>
        /// 請求付款時，回應的商家唯一訂單編號
        /// </summary>
        [JsonProperty("orderId")]
        public string OrderId { get; set; }

        /// <summary>
        /// 作為請求付款的結果，回應的交易序號（19個字符）
        /// </summary>
        [JsonProperty("transactionId")]
        public long TransactionId { get; set; }

        /// <summary>
        /// 授權過期時間（ISO 8601）
        /// 僅限於完成授權（capture=false）的付款，進行回傳。
        /// </summary>
        [JsonProperty("authorizationExpireDate")]
        public string AuthorizationExpireDate { get; set; }

        /// <summary>
        /// 用於自動付款的密鑰（15個字符）
        /// </summary>
        [JsonProperty("regKey")]
        public string RegKey { get; set; }

        [JsonProperty("payInfo")]
        public List<PayInfo> PayInfo { get; set; }

        [JsonProperty("packages")]
        public List<Packages> Packages { get; set; }

        [JsonProperty("merchantReference")]
        public MerchantReference MerchantReference { get; set; }

        [JsonProperty("shipping")]
        public Shipping Shipping { get; set; }
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

        /// <summary>
        /// 用於自動付款的信用卡別名
        /// 綁定在LINE Pay的信用卡名。一般在綁定信用卡時設置。
        /// 如果LINE Pay用戶沒有設置別名，會回傳空字符串。
        /// 用戶通過LINE Pay可以更改別名，更改內容不會分享到商家。
        /// </summary>
        [JsonProperty("creditCardNickname")]
        public string CreditCardNickname { get; set; }

        /// <summary>
        /// 用於自動付款的信用卡品牌
        /// VISA
        /// MASTER
        /// AMEX
        /// DINERS
        /// JCB
        /// </summary>
        [JsonProperty("creditCardBrand")]
        public string CreditCardBrand { get; set; }

        /// <summary>
        /// 被遮罩（Masking）的信用卡號（僅限於台灣商家回應，若您需要，可以向商家中心管理者申請獲取。）
        /// </summary>
        [JsonProperty("maskedCreditCardNumber")]
        public string MaskedCreditCardNumber { get; set; }
    }


    public class MerchantReference
    {
        [JsonProperty("affiliateCodes")]
        public AffiliateCodes AffiliateCodes { get; set; }
    }

    public class AffiliateCodes
    {
        /// <summary>
        /// 交易中若用戶符合商店支援的卡片類型
        /// -MOBILE_CARRIER(電子發票載具)
        ///(功能預設不開啟)
        /// </summary>
        [JsonProperty("cardType")]
        public List<string> CardType { get; set; }

        /// <summary>
        /// 交易中若用戶符合商店支援的卡片類型所對應的內容值
        /// </summary>
        [JsonProperty("cardId")]
        public List<string> CardId { get; set; }
    }
}
