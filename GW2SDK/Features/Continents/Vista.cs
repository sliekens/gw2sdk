using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [DebuggerDisplay("{Name,nq}")]
    public sealed class Vista : PointOfInterest
    {
        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public string Name { get; set; }
    }
}