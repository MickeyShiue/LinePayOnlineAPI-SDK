using System.Collections.Generic;
using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class AddFriends
    {
        /// <summary>
        /// 新增好友的服務類型
        /// line@
        /// </summary>
        [JsonProperty("type")]
        public string Type { get; set; }

        /// <summary>
        /// 各服務類型的ID list
        /// </summary>
        [JsonProperty("ids")]
        public List<string> Ids { get; set; }
    }
}
