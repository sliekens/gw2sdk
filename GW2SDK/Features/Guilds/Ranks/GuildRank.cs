namespace GuildWars2.Guilds.Ranks;

[PublicAPI]
[DataTransferObject]
public sealed record GuildRank
{
    public required string Id { get; init; }

    public required int Order { get; init; }

    public required IReadOnlyCollection<GuildPermission> Permissions { get; init; }

    public required string IconHref { get; init; }
}
