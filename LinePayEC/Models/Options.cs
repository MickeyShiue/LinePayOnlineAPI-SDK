using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class Options
    {
        [JsonProperty("payment")]
        public Payment Payment { get; set; }

        [JsonProperty("display")]
        public Display Display { get; set; }

        [JsonProperty("shipping")]
        public Shipping Shipping { get; set; }

        [JsonProperty("familyService")]
        public FamilyService FamilyService { get; set; }

        [JsonProperty("extra")]
        public Extra Extra { get; set; }
    }
}
