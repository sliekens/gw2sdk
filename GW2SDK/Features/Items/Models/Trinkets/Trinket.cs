namespace GuildWars2.Items;

[PublicAPI]
[Inheritable]
public record Trinket : Item
{
    public required IReadOnlyList<InfusionSlot> InfusionSlots { get; init; }

    public required double AttributeAdjustment { get; init; }

    public required InfixUpgrade? Prefix { get; init; }

    public required int? SuffixItemId { get; init; }

    public required IReadOnlyCollection<int>? StatChoices { get; init; }

    public required IReadOnlyCollection<ItemUpgrade>? UpgradesInto { get; init; }

    public required IReadOnlyCollection<ItemUpgrade>? UpgradesFrom { get; init; }
}
