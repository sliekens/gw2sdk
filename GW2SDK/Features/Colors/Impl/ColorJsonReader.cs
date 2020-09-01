using System;
using System.Text.Json;
using GW2SDK.Enums;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Colors.Impl
{
    public sealed class ColorJsonReader : JsonObjectReader<Color>
    {
        private ColorJsonReader()
        {
            Configure(MapColor);
        }

        public static IJsonReader<Color> Instance { get; } = new ColorJsonReader();

        private static void MapColor(JsonObjectMapping<Color> color)
        {
            color.Map("id", to => to.Id);
            color.Map("name", to => to.Name);
            color.Map("base_rgb", to => to.BaseRgb);
            color.Map("cloth", to => to.Cloth, cloth => MapColorInfo(cloth!));
            color.Map("leather", to => to.Leather, leather => MapColorInfo(leather!));
            color.Map("metal", to => to.Metal, metal => MapColorInfo(metal!));
            color.Map("fur", to => to.Fur, fur => MapColorInfo(fur!), MappingSignificance.Optional);
            color.Map("item", to => to.Item);

            // TODO: map color categories to a value object with 3 enum properties instead of a mixed enum collection
            // Hue: Gray, Brown, Red, Orange, Yellow, Green, Blue, Purple
            // Material: Vibrant, Leather, Metal
            // Rarity: Starter, Common, Uncommon, Rare, Exclusive
            color.Map(
                "categories",
                to => to.Categories,
                (in JsonElement element, in JsonPath path) => Enum.Parse<ColorCategoryName>(element.GetString(), false)
            );
        }

        private static void MapColorInfo(JsonObjectMapping<ColorInfo> colorInfo)
        {
            colorInfo.Map("brightness", to => to.Brightness);
            colorInfo.Map("contrast", to => to.Contrast);
            colorInfo.Map("hue", to => to.Hue);
            colorInfo.Map("saturation", to => to.Saturation);
            colorInfo.Map("lightness", to => to.Lightness);
            colorInfo.Map("rgb", to => to.Rgb);
        }
    }
}
