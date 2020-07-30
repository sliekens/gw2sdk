using GW2SDK.Enums;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Impl.JsonReaders.Mappings;

namespace GW2SDK.Colors.Impl
{
    public sealed class ColorJsonReader : JsonObjectReader<Color>
    {
        private ColorJsonReader()
        {
            Configure(
                color =>
                {
                    color.Map("id",       to => to.Id);
                    color.Map("name",     to => to.Name);
                    color.Map("base_rgb", to => to.BaseRgb);
                    color.Map("cloth",    to => to.Cloth,   ColorInfoJsonReader.Instance);
                    color.Map("leather",  to => to.Leather, ColorInfoJsonReader.Instance);
                    color.Map("metal",    to => to.Metal,   ColorInfoJsonReader.Instance);
                    color.Map("fur",      to => to.Fur,     ColorInfoJsonReader.Instance, MappingSignificance.Optional);
                    color.Map("item",     to => to.Item);

                    // TODO: map color categories to a value object with 3 enum properties instead of a mixed enum collection
                    // Hue: Gray, Brown, Red, Orange, Yellow, Green, Blue, Purple
                    // Material: Vibrant, Leather, Metal
                    // Rarity: Starter, Common, Uncommon, Rare, Exclusive
                    color.Map("categories", to => to.Categories, new JsonStringEnumReader<ColorCategoryName>());
                }
            );
        }

        public static IJsonReader<Color> Instance { get; } = new ColorJsonReader();
    }
}
