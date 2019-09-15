using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using Newtonsoft.Json;

namespace GW2SDK.Colors
{
    [PublicAPI]
    [DebuggerDisplay("{Name,nq}")]
    [DataTransferObject]
    public sealed class Color
    {
        [JsonProperty(Required = Required.Always)]
        public int Id { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public int[] BaseRgb { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public ColorInfo Cloth { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public ColorInfo Leather { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public ColorInfo Metal { get; set; }

        [CanBeNull]
        [JsonProperty(Required = Required.DisallowNull)]
        public ColorInfo Fur { get; set; }

        [JsonProperty(Required = Required.DisallowNull)]
        public int? Item { get; set; }

        [NotNull]
        [JsonProperty(Required = Required.Always)]
        public ColorCategoryName[] Categories { get; set; }
    }
}
