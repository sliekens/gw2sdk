﻿using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Skins
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
