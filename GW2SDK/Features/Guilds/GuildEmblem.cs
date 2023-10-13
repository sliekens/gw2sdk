namespace GuildWars2.Guilds;

[PublicAPI]
[DataTransferObject]
public sealed record GuildEmblem
{
    public required GuildEmblemPart Background { get; init; }

    public required GuildEmblemPart Foreground { get; init; }

    public required IReadOnlyCollection<GuildEmblemFlag> Flags { get; init; }
}

