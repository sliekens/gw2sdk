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

        [NotNull]
        [JsonProperty("texture_dims", Required = Required.Always)]
        public int[] TextureDimensions { get; set; }

        [CanBeNull]
        [ItemNotNull]
        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int[][] ClampedView { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public Dictionary<int, Region> Regions { get; set; }
    }
}
