using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [DebuggerDisplay("{Name,nq}")]
    public sealed class Landmark : PointOfInterest
    {
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }
    }
}