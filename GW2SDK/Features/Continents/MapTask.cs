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

        [JsonProperty(Required = Required.Always)]
        public string Objective { get; set; } = "";
        
        [JsonProperty(Required = Required.Always)]
        public int Level { get; set; }

        [JsonProperty("coord", Required = Required.Always)]
        public double[] Coordinates { get; set; } = new double[0];
        
        [JsonProperty("bounds", Required = Required.Always)]
        public double[][] Boundaries { get; set; } = new double[0][];

        [JsonProperty(Required = Required.Always)]
        public string ChatLink { get; set; } = "";
    }
}