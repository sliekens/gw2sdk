using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class GodShrine
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("poi_id", Required = Required.Always)]
        public int PointOfInterestId { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string NameContested { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string Icon { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public string IconContested { get; set; } = "";

        [JsonProperty("coord", Required = Required.Always)]
        public double[] Coordinates { get; set; } = new double[0];
    }
}