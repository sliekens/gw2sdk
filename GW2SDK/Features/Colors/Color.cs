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

        [JsonProperty(Required = Required.Always)]
        public string Name { get; set; } = "";

        [JsonProperty(Required = Required.Always)]
        public int[] BaseRgb { get; set; } = new int[0];

        [JsonProperty(Required = Required.Always)]
        public ColorInfo Cloth { get; set; } = new ColorInfo();

        [JsonProperty(Required = Required.Always)]
        public ColorInfo Leather { get; set; } = new ColorInfo();

        [JsonProperty(Required = Required.Always)]
        public ColorInfo Metal { get; set; } = new ColorInfo();

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ColorInfo? Fur { get; set; }

        [JsonProperty(Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public int? Item { get; set; }

        [JsonProperty(Required = Required.Always)]
        public ColorCategoryName[] Categories { get; set; } = new ColorCategoryName[0];
    }
}
