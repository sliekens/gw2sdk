using System.Diagnostics;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [DebuggerDisplay("{Name,nq}")]
    public sealed class Landmark : PointOfInterest
    {
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";
    }
}