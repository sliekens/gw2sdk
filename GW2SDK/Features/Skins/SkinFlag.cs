using GW2SDK.Annotations;

namespace GW2SDK.Skins
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
