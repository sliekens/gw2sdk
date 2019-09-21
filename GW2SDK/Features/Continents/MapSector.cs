using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class MapSector
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string Name { get; set; }
        
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [NotNull]
        [JsonProperty("coord", Required = Required.Always)]
        public double[] Coordinates { get; set; }

        [NotNull]
        [ItemNotNull]
        [JsonProperty("bounds", Required = Required.Always)]
        public double[][] Boundaries { get; set; }
        
        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string ChatLink { get; set; }
    }
}