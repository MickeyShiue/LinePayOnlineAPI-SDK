using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class Recipient
    {
        /// <summary>
        /// 收貨人名
        /// </summary>
        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        /// <summary>
        /// 收貨人姓
        /// </summary>
        [JsonProperty("lastName")]
        public string LastName { get; set; }

        /// <summary>
        /// 詳細名資訊
        /// </summary>
        [JsonProperty("firstNameOptional")]
        public string FirstNameOptional { get; set; }

        /// <summary>
        /// 詳細姓資訊
        /// </summary>
        [JsonProperty("lastNameOptional")]
        public string LastNameOptional { get; set; }

        /// <summary>
        /// 收貨人電子郵件
        /// </summary>
        [JsonProperty("email")]
        public string Email { get; set; }

        /// <summary>
        /// 收貨人電話號碼
        /// </summary>
        [JsonProperty("phoneNo")]
        public string PhoneNo { get; set; }
    }
}
