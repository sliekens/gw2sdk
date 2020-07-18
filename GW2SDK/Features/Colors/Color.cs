using System.Diagnostics;
using GW2SDK.Annotations;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonReaders;
using Newtonsoft.Json;

namespace GW2SDK.Colors
{
    public sealed class ColorInfoJsonReader : JsonObjectReader<ColorInfo>
    {
        public static JsonObjectReader<ColorInfo> Instance { get; } = new ColorInfoJsonReader();

        private ColorInfoJsonReader()
        {
            Map("brightness", to => to.Brightness);
            Map("contrast", to => to.Contrast);
            Map("hue", to => to.Hue);
            Map("saturation", to => to.Saturation);
            Map("lightness", to => to.Lightness);
            Map("rgb", to => to.Rgb);
        }
    }
    public sealed class ColorJsonReader : JsonObjectReader<Color>
    {
        public static JsonObjectReader<Color> Instance { get; } = new ColorJsonReader();

        private ColorJsonReader()
        {
            Map("id", to => to.Id);
            Map("name", to => to.Name);
            Map("base_rgb", to => to.BaseRgb);
            Map("cloth", to => to.Cloth, ColorInfoJsonReader.Instance);
            Map("leather", to => to.Leather, ColorInfoJsonReader.Instance);
            Map("metal", to => to.Metal, ColorInfoJsonReader.Instance);
            Map("fur", to => to.Fur, ColorInfoJsonReader.Instance, PropertySignificance.Optional);
            Map("item", to => to.Item);

            // TODO: map color categories to a value object with 3 enum properties instead of a mixed enum collection
            // Hue: Gray, Brown, Red, Orange, Yellow, Green, Blue, Purple
            // Material: Vibrant, Leather, Metal
            // Rarity: Starter, Common, Uncommon, Rare, Exclusive
            Map("categories", to => to.Categories, new JsonStringEnumReader<ColorCategoryName>());
        }
    }

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
