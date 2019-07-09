using GW2SDK.Annotations;

namespace GW2SDK.Enums
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
