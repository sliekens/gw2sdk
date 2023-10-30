using GuildWars2.ItemStats;

namespace GuildWars2.Inventories;

[PublicAPI]
[DataTransferObject]
public sealed record ItemSlot
{
    public required int Id { get; init; }

    public required int Count { get; init; }

    public required int? Charges { get; init; }

    public required int? SkinId { get; init; }

    public required IReadOnlyList<int>? UpgradeItemIds { get; init; }

    /// <summary>Indicates which upgrade slots are in use. (0-based)</summary>
    public required IReadOnlyList<int>? UpgradeSlotIndices { get; init; }

    public required IReadOnlyList<int>? InfusionItemIds { get; init; }

    // Always length 4
    public required IReadOnlyList<int?>? DyeIds { get; init; }

    public required ItemBinding Binding { get; init; }

    /// <summary>The name of the character when the item is Soulbound.</summary>
    public required string BoundTo { get; init; }

    public required SelectedStat? Stats { get; init; }
}
