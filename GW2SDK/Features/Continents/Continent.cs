using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Continent
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty("continent_dims", Required = Required.Always)]
        public int[] ContinentDimensions { get; set; } = new int[0];

        [JsonProperty(Required = Required.Always)]
        public int MinZoom { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int MaxZoom { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int[] Floors { get; set; } = new int[0];
    }
}
