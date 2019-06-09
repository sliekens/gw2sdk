namespace GW2SDK.Features.Items
{
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
