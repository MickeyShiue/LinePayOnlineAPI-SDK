using Newtonsoft.Json;

namespace LinePayEC.Models.ResponseModels.CheckPaymentStatusResponse
{
    public class CheckPaymentStatusResponse
    {
        [JsonProperty("returnCode")]
        public string ReturnCode { get; set; }

        [JsonProperty("returnMessage")]
        public string ReturnMessage { get; set; }

        [JsonProperty("info")]
        public Info Info { get; set; }
    }

    public class Info
    {
        [JsonProperty("shipping")]
        public Shipping Shipping { get; set; }
    }

    public class Shipping
    {
        [JsonProperty("methodId")]
        public string MethodId { get; set; }


        [JsonProperty("feeAmount")]
        public int FeeAmount { get; set; }
    }
}
