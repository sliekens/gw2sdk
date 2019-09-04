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

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty("continent_dims", Required = Required.Always)]
        public int[] ContinentDimensions { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int MinZoom { get; set; }

        [JsonProperty(Required = Required.Always)]
        public int MaxZoom { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public int[] Floors { get; set; }
    }
}
