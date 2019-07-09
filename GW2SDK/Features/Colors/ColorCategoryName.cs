using GW2SDK.Annotations;

namespace GW2SDK.Colors
{
    [PublicAPI]
    public enum ColorCategoryName
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(ColorCategory)
        Blue = 1,

        Brown,

        Common,

        Exclusive,

        Gray,

        Green,

        Leather,

        Metal,

        Orange,

        Purple,

        Rare,

        Red,

        Starter,

        Uncommon,

        Vibrant,

        Yellow
    }
}