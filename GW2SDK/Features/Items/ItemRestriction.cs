using GW2SDK.Annotations;

namespace GW2SDK.Items
{
    [PublicAPI]
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