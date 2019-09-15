using System.Collections.Generic;
using System.Diagnostics;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject(RootObject = false)]
    public sealed class Region
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty("label_coord", Required = Required.Always)]
        public int[] LabelCoordinates { get; set; }

        [NotNull]
        [JsonProperty("continent_rect", Required = Required.Always)]
        public int[][] ContinentRectangle { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, Map> Maps { get; set; }
    }
}