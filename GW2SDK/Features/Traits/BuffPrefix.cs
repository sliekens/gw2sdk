using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Traits
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed class BuffPrefix
    {
        [JsonProperty("text", Required = Required.Always)]
        public string Text { get; set; } = "";

        [JsonProperty("icon", Required = Required.Always)]
        public string Icon { get; set; } = "";

        [JsonProperty("status", Required = Required.DisallowNull)]
        public string? Status { get; set; }

        [JsonProperty("description", Required = Required.DisallowNull)]
        public string? Description { get; set; }
    }
}