using GuildWars2.Items;

namespace GuildWars2.Hero.Equipment.Wardrobe;

[PublicAPI]
[Inheritable]
public record ArmorSkin : Skin
{
    public required WeightClass WeightClass { get; init; }

    public required DyeSlotInfo? DyeSlots { get; init; }
}
