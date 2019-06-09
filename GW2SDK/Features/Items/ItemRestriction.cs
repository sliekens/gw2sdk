namespace GW2SDK.Features.Items
{
    public enum ItemRestriction
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(ItemRestriction)
        Asura = 1,

        Charr,

        Female,

        Human,

        Mesmer,

        Necromancer,

        Norn,

        Ranger,

        Sylvari
    }
}