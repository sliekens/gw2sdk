namespace GuildWars2.Hero.Wardrobe;

[PublicAPI]
[Inheritable]
public record ArmorSkin : Skin
{
    public required WeightClass WeightClass { get; init; }

    public required DyeSlotInfo? DyeSlots { get; init; }
}
