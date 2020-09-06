using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class Payment
    {
        /// <summary>
        /// 是否自動請款
        /// true(預設)：呼叫Confirm API，統一進行授權/請款處理
        /// false：呼叫Confirm API只能完成授權，需要呼叫Capture API完成請款
        /// </summary>
        [JsonProperty("capture")]
        public bool Capture { get; set; }

        /// <summary>
        /// 付款類型
        /// NORMAL
        /// PREAPPROVED
        /// </summary>
        [JsonProperty("payType")]
        public string PayType { get; set; }
    }
}


