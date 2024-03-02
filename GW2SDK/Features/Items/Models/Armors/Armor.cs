namespace GuildWars2.Items;

[PublicAPI]
[Inheritable]
public record Armor : Item
{
    public required int DefaultSkinId { get; init; }

    public required WeightClass WeightClass { get; init; }

    public required int Defense { get; init; }

    public required IReadOnlyList<InfusionSlot> InfusionSlots { get; init; }

    public required double AttributeAdjustment { get; init; }

    public required InfixUpgrade? Prefix { get; init; }

    public required int? SuffixItemId { get; init; }

    public required IReadOnlyList<int>? StatChoices { get; init; }
}
