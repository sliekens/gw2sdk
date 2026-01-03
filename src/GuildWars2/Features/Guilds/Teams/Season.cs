namespace GuildWars2.Guilds.Teams;

/// <summary>Information about a guild team results in a PvP season.</summary>
[DataTransferObject]
public sealed record Season
{
    /// <summary>The ID of the PvP season.</summary>
    public required string Id { get; init; }

    /// <summary>The number of wins the team has in this season.</summary>
    public required int Wins { get; init; }

    /// <summary>The number of losses the team has in this season.</summary>
    public required int Losses { get; init; }

    /// <summary>The rating of the team in this season.</summary>
    public required int Rating { get; init; }
}
