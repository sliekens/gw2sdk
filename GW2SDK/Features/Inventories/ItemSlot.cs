using GuildWars2.ItemStats;

namespace GuildWars2.Inventories;

/// <summary>Information about an item slot in the inventory or bank.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record ItemSlot
{
    /// <summary>The ID of the item in this slot.</summary>
    public required int Id { get; init; }

    /// <summary>How many of the item are in this slot.</summary>
    public required int Count { get; init; }

    /// <summary>If present, indicates the number of charges remaining on the item. Used for breakable gathering tools and
    /// salvage kits.</summary>
    public required int? Charges { get; init; }

    /// <summary>If present, indicates the ID of the skin applied to the item. If the item has no skin applied, this value is
    /// <c>null</c>.</summary>
    public required int? SkinId { get; init; }

    /// <summary>The item IDs of runes or sigils in this item.</summary>
    public required IReadOnlyList<int>? UpgradeItemIds { get; init; }

    /// <summary>The slot occupied by the upgrade at the corresponding position in <see cref="UpgradeItemIds" />. Starts at 0.</summary>
    public required IReadOnlyList<int>? UpgradeSlotIndices { get; init; }

    /// <summary>The item IDs of infusions in this item.</summary>
    public required IReadOnlyList<int>? InfusionItemIds { get; init; }

    // Always length 4
    /// <summary>The IDs of colors applied to the current item.</summary>
    public required IReadOnlyList<int?>? DyeIds { get; init; }

    /// <summary>Whether this item is bound.</summary>
    public required ItemBinding Binding { get; init; }

    /// <summary>The name of the character if the item is soulbound.</summary>
    public required string BoundTo { get; init; }

    /// <summary>The attribute combination for items with selectable stats.</summary>
    public required SelectedStat? Stats { get; init; }
}
