using System.Collections.Generic;
using GW2SDK.Annotations;
using Newtonsoft.Json;

namespace GW2SDK.Continents
{
    [PublicAPI]
    [DataTransferObject]
    public sealed class Floor
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [JsonProperty("texture_dims", Required = Required.Always)]
        public int[] TextureDimensions { get; set; } = new int[0];

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int[][]? ClampedView { get; set; }

        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, Region> Regions { get; set; } = new Dictionary<int, Region>(0);
    }
}
