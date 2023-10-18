namespace GuildWars2.Guilds.Teams;

[PublicAPI]
[DataTransferObject]
public sealed record GuildTeam
{
    public required int Id { get; init; }

    public required IReadOnlyList<GuildTeamMember> Members { get; init; }

    public required string Name { get; init; }

    public required GuildTeamState State { get; init; }

    public required Results Aggregate { get; init; }

    public required Ladders Ladders { get; init; }

    public required IReadOnlyList<Game> Games { get; init; }

    public required IReadOnlyList<Season> Seasons { get; init; }
}
