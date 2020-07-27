using GW2SDK.Enums;
using GW2SDK.Impl.JsonReaders;

namespace GW2SDK.Colors.Impl
{
    public sealed class ColorJsonReader : JsonObjectReader<Color>
    {
        public static JsonObjectReader<Color> Instance { get; } = new ColorJsonReader();

        private ColorJsonReader()
        {
            Map("id",       to => to.Id);
            Map("name",     to => to.Name);
            Map("base_rgb", to => to.BaseRgb);
            Map("cloth",    to => to.Cloth,   ColorInfoJsonReader.Instance);
            Map("leather",  to => to.Leather, ColorInfoJsonReader.Instance);
            Map("metal",    to => to.Metal,   ColorInfoJsonReader.Instance);
            Map("fur",      to => to.Fur,     ColorInfoJsonReader.Instance, MappingSignificance.Optional);
            Map("item",     to => to.Item);

            // TODO: map color categories to a value object with 3 enum properties instead of a mixed enum collection
            // Hue: Gray, Brown, Red, Orange, Yellow, Green, Blue, Purple
            // Material: Vibrant, Leather, Metal
            // Rarity: Starter, Common, Uncommon, Rare, Exclusive
            Map("categories", to => to.Categories, new JsonStringEnumReader<ColorCategoryName>());
            Compile();
        }
    }
}