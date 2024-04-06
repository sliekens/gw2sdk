﻿using GuildWars2.Chat;
using GuildWars2.Hero;

namespace GuildWars2.Items;

/// <summary>Information about an item. This type is the base type for all items. Cast objects of this type to a more
/// specific type to access more properties.</summary>
[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Item
{
    /// <summary>The item ID.</summary>
    public required int Id { get; init; }

    /// <summary>The name of the item.</summary>
    public required string Name { get; init; }

    /// <summary>The description of the item as it appears in the tooltip.</summary>
    public required string Description { get; init; }

    /// <summary>The minimum level required to use the item.</summary>
    public required int Level { get; init; }

    /// <summary>The item rarity.</summary>
    public required Extensible<Rarity> Rarity { get; init; }

    /// <summary>The unit price of the item when sold to a merchant. Items will not appear in a sell-to-vendor list when this
    /// value is <see cref="Coin.Zero" />, or when the <see cref="ItemFlags.NoSell" /> flag is set.</summary>
    public required Coin VendorValue { get; init; }

    /// <summary>The game types in which the items can be used.</summary>
    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<Extensible<GameType>> GameTypes { get; init; }

    /// <summary>Contains various modifiers.</summary>
    public required ItemFlags Flags { get; init; }

    /// <summary>The character restrictions for the item.</summary>
    public required ItemRestriction Restrictions { get; init; }

    /// <summary>The chat code of the item. This can be used to link the item in the chat, but also in guild or squad messages.</summary>
    public required string ChatLink { get; init; }

    /// <summary>The URL of the item icon.</summary>
    public required string? IconHref { get; init; }

    /// <summary>Gets a chat link object for this item.</summary>
    /// <returns>The chat link as an object.</returns>
    public ItemLink GetChatLink() =>
        new()
        {
            ItemId = Id,
            Count = 1,
            SkinId = null,
            SuffixItemId = null,
            SecondarySuffixItemId = null
        };
}
