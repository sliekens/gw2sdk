namespace GuildWars2.Items;

[PublicAPI]
public sealed record Miniature : Item
{
    public required int MiniatureId { get; init; }
}
