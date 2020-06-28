using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Impl.V2
{
    [DataTransferObject]
    internal sealed class ApiInfo
    {
        [JsonProperty("langs", Required = Required.Always)]
        public string[] Languages { get; set; } = new string[0];

        [JsonProperty("routes", Required = Required.Always)]
        public ApiRoute[] Routes { get; set; } = new ApiRoute[0];

        [JsonProperty("schema_versions", Required = Required.Always)]
        public ApiVersion[] SchemaVersions { get; set; } = new ApiVersion[0];
    }
}
