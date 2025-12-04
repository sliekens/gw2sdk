namespace GuildWars2.Guilds.Teams;

/// <summary>Information about a team's results.</summary>
[DataTransferObject]
public sealed record Results
{
    /// <summary>The number of wins the team has.</summary>
    public required int Wins { get; init; }

    /// <summary>The number of losses the team has.</summary>
    public required int Losses { get; init; }

    /// <summary>The number of times the teams deserted.</summary>
    public required int Desertions { get; init; }

    /// <summary>The number of times the team has received a Bye.</summary>
    public required int Byes { get; init; }

    /// <summary>The number of times the team has forfeited.</summary>
    public required int Forfeits { get; init; }
}
