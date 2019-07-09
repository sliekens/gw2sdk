using GW2SDK.Annotations;

namespace GW2SDK.Enums
{
    [PublicAPI]
    public enum SkinFlag
    {
        // Skip index 0 so that Enum.IsDefined returns false for default(SkinFlag)
        HideIfLocked = 1,

        NoCost,

        OverrideRarity,

        ShowInWardrobe
    }
}
