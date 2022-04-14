using GW2SDK.Annotations;

using JetBrains.Annotations;

namespace GW2SDK.Skins.Models;

[PublicAPI]
[Inheritable]
public record ArmorSkin : Skin
{
    public WeightClass WeightClass { get; init; }

    public DyeSlotInfo? DyeSlots { get; init; }
}