using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Items
{
    [PublicAPI]
    public enum ItemRarity
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(ItemRarity)
        Junk = 1,

        Basic,

        Fine,

        Masterwork,

        Rare,

        Exotic,

        Ascended,

        Legendary
    }
}
