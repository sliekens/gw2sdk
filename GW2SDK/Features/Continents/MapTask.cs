using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DebuggerDisplay("{Objective,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class MapTask
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Objective { get; set; }
        
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