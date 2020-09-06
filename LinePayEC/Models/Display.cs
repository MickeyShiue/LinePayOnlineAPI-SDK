using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class Display
    {
        /// <summary>
        /// 等待付款頁的語言程式碼，預設為英文（en）
        /// 支援語言：en、ja、ko、th、zh_TW、zh_CN
        /// </summary>
        [JsonProperty("locale")]
        public string Locale { get; set; }

        /// <summary>
        /// 檢查將用於訪問confirmUrl的瀏覽器
        /// true：如果跟請求付款的瀏覽器不同，引導使用LINE Pay請求付款的瀏覽器
        /// false：無需檢查瀏覽器，直接訪問confirmUrl
        /// </summary>
        [JsonProperty("checkConfirmUrlBrowser")]
        public bool CheckConfirmUrlBrowser { get; set; }
    }
}
