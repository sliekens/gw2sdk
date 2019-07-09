using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    public enum ColorCategoryName
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(ColorCategoryName)
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
