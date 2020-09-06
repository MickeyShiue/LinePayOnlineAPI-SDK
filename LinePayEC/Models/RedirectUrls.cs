using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class RedirectUrls
    {
        /// <summary>
        /// 在Android環境切換應用時所需的資訊，用於防止網路釣魚攻擊（phishing）
        /// </summary>
        [JsonProperty("appPackageName")]
        public string AppPackageName { get; set; }

        /// <summary>
        /// 使用者授權付款後，跳轉到該商家URL
        /// </summary>
        [JsonProperty("confirmUrl")]
        [Required]
        public string ConfirmUrl { get; set; }

        /// <summary>
        /// 使用者授權付款後，跳轉的confirmUrl類型
        /// </summary>
        [JsonProperty("confirmUrlType")]
        public string ConfirmUrlType { get; set; }

        /// <summary>
        /// 使用者通過LINE付款頁，取消付款後跳轉到該URL
        /// </summary>
        [JsonProperty("cancelUrl")]
        [Required]
        public string CancelUrl { get; set; }
    }
}
