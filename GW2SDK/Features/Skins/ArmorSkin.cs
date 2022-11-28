using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Skins;

[PublicAPI]
[Inheritable]
public record ArmorSkin : Skin
{
    public required WeightClass WeightClass { get; init; }

    public required DyeSlotInfo? DyeSlots { get; init; }
}
