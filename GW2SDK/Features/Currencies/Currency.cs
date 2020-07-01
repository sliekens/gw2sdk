using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Currencies
{
    [PublicAPI]
    [DataTransferObject]
    [DebuggerDisplay("{Name,nq}")]
    public sealed class Currency
    {
        [JsonProperty("id", Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty("description", Required = Required.Always)]
        public string Description { get; set; } = "";

        [JsonProperty("order", Required = Required.Always)]
        public int Order { get; set; }

        [JsonProperty("icon", Required = Required.Always)]
        public string Icon { get; set; } = "";
    }
}
