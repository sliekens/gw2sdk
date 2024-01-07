﻿using GuildWars2.Chat;

namespace GuildWars2.Items;

[PublicAPI]
[Inheritable]
[DataTransferObject]
public record Item
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }

    public required int Level { get; init; }

    public required Rarity Rarity { get; init; }

    public required Coin VendorValue { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<GameType> GameTypes { get; init; }

    public required ItemFlags Flags { get; init; }

    /// <remarks>Can be empty.</remarks>
    public required IReadOnlyCollection<ItemRestriction> Restrictions { get; init; }

    /// <summary>The chat code of the item. This can be used to link the item in the chat, but also in guild or squad messages.</summary>
    public required string ChatLink { get; init; }

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
