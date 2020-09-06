using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class Products
    {
        /// <summary>
        /// 商家商品ID
        /// </summary>
        [JsonProperty("id")]
        public string Id { get; set; }

        /// <summary>
        /// 商品名
        /// </summary>
        [Required]
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <summary>
        /// 商品圖示的URL,
        /// </summary>
        [JsonProperty("imageUrl")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// 商品數量
        /// </summary>
        [JsonProperty("quantity")]
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// 各商品付款金額
        /// </summary>
        [JsonProperty("price")]
        [Required]
        public int Price { get; set; }

        /// <summary>
        /// 各商品原金額
        /// </summary>
        [JsonProperty("originalPrice")]
        public int OriginalPrice { get; set; }
    }
}
