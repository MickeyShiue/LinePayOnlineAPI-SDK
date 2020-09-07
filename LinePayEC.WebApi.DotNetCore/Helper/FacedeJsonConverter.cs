using Newtonsoft.Json;

namespace LinePayEC.WebApi.DotNetCore.Helper
{
    public class JsonConverterFacade
    {
        public static T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public static string SerializeObject(object source)
        {
            return JsonConvert.SerializeObject(source);
        }

        public static string SerializeObject(object source, JsonSerializerSettings serializerSettings)
        {
            return JsonConvert.SerializeObject(source, serializerSettings);
        }
    }
}
