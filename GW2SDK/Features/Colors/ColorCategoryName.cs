using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Colors
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