using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class Packages
    {
        /// <summary>
        /// Package list的唯一ID
        /// </summary>
        [JsonProperty("id")]
        [Required]
        public string Id { get; set; }

        /// <summary>
        /// 一個Package中的商品總價 = sum(products[].quantity * products[].price)
        /// </summary>
        [JsonProperty("amount")]
        [Required]
        public int Amount { get; set; }

        /// <summary>
        /// 手續費：在付款金額中含手續費時設定
        /// </summary>
        [JsonProperty("userFee")]
        public int UserFee { get; set; }

        /// <summary>
        /// Package名稱 （or Shop Name）
        /// </summary>
        [JsonProperty("name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// Products 物件
        /// </summary>
        [JsonProperty("products")]
        public List<Products> Products { get; set; }
    }
}
