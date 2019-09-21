using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [DebuggerDisplay("{Name,nq}")]
    public sealed class Waypoint : PointOfInterest
    {
        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
    }
}