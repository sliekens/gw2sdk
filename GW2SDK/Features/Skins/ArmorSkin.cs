using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Skins;

[PublicAPI]
[Inheritable]
public record ArmorSkin : Skin
{
    public required WeightClass WeightClass { get; init; }

    public required DyeSlotInfo? DyeSlots { get; init; }
}
