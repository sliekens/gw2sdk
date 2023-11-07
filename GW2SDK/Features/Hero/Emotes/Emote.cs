namespace GuildWars2.Hero.Emotes;

[PublicAPI]
[DataTransferObject]
public sealed record Emote
{
    public required string Id { get; init; }

    public required IReadOnlyCollection<string> Commands { get; init; }

    public required IReadOnlyCollection<int> UnlockItems { get; init; }
}
