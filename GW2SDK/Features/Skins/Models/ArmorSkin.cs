﻿using GW2SDK.Annotations;

namespace GW2SDK.Skins
{
    [PublicAPI]
    [Inheritable]
    public record ArmorSkin : Skin
    {
        public WeightClass WeightClass { get; init; }

        public DyeSlotInfo? DyeSlots { get; init; }
    }
}
