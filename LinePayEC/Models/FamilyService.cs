using Newtonsoft.Json;

namespace LinePayEC.Models
{
    public class FamilyService
    {
        [JsonProperty("addFriends")]
        public AddFriends AddFriends { get; set; }
    }
}
