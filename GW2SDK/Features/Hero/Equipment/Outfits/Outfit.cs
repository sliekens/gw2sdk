using GuildWars2.Chat;

namespace GuildWars2.Hero.Equipment.Outfits;

[PublicAPI]
[DataTransferObject]
public sealed record Outfit
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    /// <summary>The URL of the outfit icon.</summary>
    public required string IconHref { get; init; }

    /// <summary>The IDs of the items that unlock the outfit when consumed.</summary>
    public required IReadOnlyCollection<int> UnlockItemIds { get; init; }

    /// <summary>Gets a chat link object for this outfit.</summary>
    /// <returns>The chat link as an object.</returns>
    public OutfitLink GetChatLink() => new() { OutfitId = Id };
}
