using GuildWars2.Hero;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record Backpack : Item
{
    public required int DefaultSkinId { get; init; }

    public required IReadOnlyList<InfusionSlot> InfusionSlots { get; init; }

    public required double AttributeAdjustment { get; init; }

    public required int? AttributeCombinationId { get; init; }

    public required IDictionary<AttributeName, int> Attributes { get; init; }

    public required Buff? Buff { get; init; }

    public required int? SuffixItemId { get; init; }

    public required IReadOnlyList<int>? StatChoices { get; init; }

    public required IReadOnlyCollection<ItemUpgrade>? UpgradesInto { get; init; }

    public required IReadOnlyCollection<ItemUpgrade>? UpgradesFrom { get; init; }
}
