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

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty("label_coord", Required = Required.Always)]
        public int[] LabelCoordinates { get; set; } = new int[0];

        [JsonProperty("continent_rect", Required = Required.Always)]
        public int[][] ContinentRectangle { get; set; } = new int[0][];

        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, Map> Maps { get; set; } = new Dictionary<int, Map>(0);
    }
}