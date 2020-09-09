using Newtonsoft.Json;

namespace LinePayEC.Models.ResponseModels.VoidResponse
{
    public class VoidResponse
    {
        /// <summary>
        /// 結果代碼
        /// 0000	成功
        /// 1101	買家不是LINE Pay的用戶
        /// 1102	買方被停止交易
        /// 1104	此商家不存在
        /// 1105	此商家無法使用LINE Pay
        /// 1106	標頭(Header)資訊錯誤
        /// 1150	交易記錄不存在
        /// 1155	該交易序號有誤
        /// 1165	該交易已經被取消授權且無效
        /// 1170	使用者帳戶的餘額有變動
        /// 1198	API重覆呼叫，或者授權更新過程中，呼叫了CaptureAPI（請幾分鐘後重試一下）
        /// 1199	內部請求錯誤
        /// 1900	發生暫時錯誤，請稍後重試
        /// 1902	發生暫時錯誤，請稍後重試
        /// 1999	跟已發出的請求資訊不同
        /// 9000	內部錯誤
        /// </summary>
        [JsonProperty("returnCode")]
        public string ReturnCode { get; set; }

        /// <summary>
        /// 結果訊息或失敗原因。有如下例子。
        /// 該商家無法交易
        /// 商家認證資訊錯誤
        /// </summary>
        [JsonProperty("returnMessage")]
        public string ReturnMessage { get; set; }
    }
}
