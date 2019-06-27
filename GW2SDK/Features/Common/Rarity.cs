using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Common
{
    [PublicAPI]
    public enum Rarity
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(Rarity)
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
