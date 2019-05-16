using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace GW2SDK.Infrastructure
{
    public static class Json
    {
        public static JsonSerializerSettings DefaultJsonSerializerSettings => new JsonSerializerSettings
        {
            ContractResolver = new DefaultContractResolver
            {
                NamingStrategy = new SnakeCaseNamingStrategy()
            }
        };
    }
}
