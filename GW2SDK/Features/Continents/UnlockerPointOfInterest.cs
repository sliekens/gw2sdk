using System.Diagnostics;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [DebuggerDisplay("{Name,nq}")]
    public sealed class UnlockerPointOfInterest : PointOfInterest
    {
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Name { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Icon { get; set; } = "";
    }
}
