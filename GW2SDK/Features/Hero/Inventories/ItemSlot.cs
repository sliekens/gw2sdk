using GuildWars2.Chat;
using GuildWars2.Hero.Equipment;

namespace GuildWars2.Hero.Inventories;

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

    /// <summary>If present, indicates the item ID of an upgrade component.</summary>
    public required int? SuffixItemId { get; init; }

    /// <summary>If present, indicates the item ID of an upgrade component in the second slot of a two-handed weapon.</summary>
    public required int? SecondarySuffixItemId { get; init; }

    /// <summary>The item IDs of infusions in this item.</summary>
    public required IReadOnlyList<int> InfusionItemIds { get; init; }

    /// <summary>The color IDs of the dyes applied to the current item.</summary>
    public required IReadOnlyList<int> DyeColorIds { get; init; }

    /// <summary>Whether this item is bound.</summary>
    public required Extensible<ItemBinding> Binding { get; init; }

    /// <summary>The name of the character if the item is soulbound.</summary>
    public required string BoundTo { get; init; }

    /// <summary>The selected attribute combination for equipment with selectable stats.</summary>
    public required SelectedAttributeCombination? Stats { get; init; }

    /// <summary>Gets a chat link object for this item slot.</summary>
    /// <returns>The chat link as an object.</returns>
    public ItemLink GetChatLink()
    {
        return new ItemLink
        {
            ItemId = Id,
            Count = Count,
            SkinId = SkinId,
            SuffixItemId = SuffixItemId,
            SecondarySuffixItemId = SecondarySuffixItemId
        };
    }
}
