using GuildWars2.Chat;

namespace GuildWars2.Hero.Equipment.Outfits;

[PublicAPI]
[DataTransferObject]
public sealed record Outfit
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required string IconHref { get; init; }

    public required IReadOnlyCollection<int> UnlockItems { get; init; }

    /// <summary>Gets a chat link object for this outfit.</summary>
    /// <returns>The chat link as an object.</returns>
    public OutfitLink GetChatLink() => new() { OutfitId = Id };
}
