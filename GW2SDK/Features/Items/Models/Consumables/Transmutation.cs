using GuildWars2.Chat;

namespace GuildWars2.Items;

[PublicAPI]
public sealed record Transmutation : Consumable
{
    public required IReadOnlyCollection<int> SkinIds { get; init; }

    /// <summary>Gets chat link objects for the skins.</summary>
    /// <returns>The chat links as objects.</returns>
    public IEnumerable<SkinLink> GetSkinChatLinks() =>
        SkinIds.Select(id => new SkinLink { SkinId = id });
}
