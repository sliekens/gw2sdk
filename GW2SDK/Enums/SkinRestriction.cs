using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    public enum SkinRestriction
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(SkinRestriction)
        Asura = 1,

        Charr,

        Human,

        Norn,

        Sylvari
    }
}
