namespace GuildWars2.Guilds.Teams;

/// <summary>Information about a guild PvP team.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record GuildTeam
{
    /// <summary>The team ID.</summary>
    public required int Id { get; init; }

    /// <summary>The team members and their role.</summary>
    public required IReadOnlyList<GuildTeamMember> Members { get; init; }

    /// <summary>The name of the team.</summary>
    public required string Name { get; init; }

    /// <summary>The active state of the team.</summary>
    public required Extensible<GuildTeamState> State { get; init; }

    /// <summary>The aggregated wins and losses of the team.</summary>
    public required Results Aggregate { get; init; }

    /// <summary>The team's results grouped by game mode.</summary>
    public required Ladders Ladders { get; init; }

    /// <summary>The games played by the team.</summary>
    public required IReadOnlyList<Game> Games { get; init; }

    /// <summary>The seasons in which the team played.</summary>
    public required IReadOnlyList<Season> Seasons { get; init; }
}
