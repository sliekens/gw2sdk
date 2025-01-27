using System.Text.Json.Serialization;
using GuildWars2.Chat;

namespace GuildWars2.Hero.Equipment.Templates;

/// <summary>Information about a single item in the character's armory.</summary>
[PublicAPI]
[DataTransferObject]
[JsonConverter(typeof(EquipmentItemJsonConverter))]
public sealed record EquipmentItem
{
    /// <summary>The item ID.</summary>
    public required int Id { get; init; }

    /// <summary>The number of this item in the armory (i.e. not currently active in any slot).</summary>
    public required int? Count { get; init; }

    /// <summary>The slot where this item is equipped.</summary>
    public required Extensible<EquipmentSlot>? Slot { get; init; }

    /// <summary>If present, indicates the item ID of an upgrade component.</summary>
    public required int? SuffixItemId { get; init; }

    /// <summary>If present, indicates the item ID of an upgrade component in the second slot of a two-handed weapon.</summary>
    public required int? SecondarySuffixItemId { get; init; }

    /// <summary>The item IDs of infusions in this item.</summary>
    public required IReadOnlyList<int> InfusionItemIds { get; init; }

    /// <summary>The skin ID.</summary>
    public required int? SkinId { get; init; }

    /// <summary>The selected attribute combination for equipment with selectable stats.</summary>
    public required SelectedAttributeCombination? Stats { get; init; }

    /// <summary>Whether this item is bound.</summary>
    public required Extensible<ItemBinding> Binding { get; init; }

    /// <summary>The name of the character if the item is soulbound.</summary>
    public required string BoundTo { get; init; }

    /// <summary>Whether this item is currently equipped or stored in the (legendary) armory.</summary>
    public required Extensible<EquipmentLocation> Location { get; init; }

    /// <summary>The equipment template numbers in which this item is (re)used.</summary>
    public required IReadOnlyList<int> TemplateNumbers { get; init; }

    /// <summary>The color IDs of dyes applied to the item.</summary>
    public required IReadOnlyList<int> DyeColorIds { get; init; }

    /// <summary>Gets a chat link object for this item.</summary>
    /// <returns>The chat link as an object.</returns>
    public ItemLink GetChatLink()
    {
        return new ItemLink
        {
            ItemId = Id,
            SkinId = SkinId,
            SuffixItemId = SuffixItemId,
            SecondarySuffixItemId = SecondarySuffixItemId
        };
    }
}
