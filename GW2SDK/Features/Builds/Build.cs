using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Builds
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class Build
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }
    }
}
