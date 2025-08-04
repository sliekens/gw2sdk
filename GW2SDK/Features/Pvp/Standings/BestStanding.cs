namespace GuildWars2.Pvp.Standings;

/// <summary>Information about the highest division standing achieved by the player in a PvP League season.</summary>
[DataTransferObject]
public sealed record BestStanding
{
    /// <summary>The highest total number of pips earned in the season.</summary>
    public required int TotalPips { get; init; }

    /// <summary>The 0-based index of the highest division reached in the season.</summary>
    public required int Division { get; init; }

    /// <summary>The 0-based index of the highest tier reached in the division indicated by <see cref="Division" />.</summary>
    public required int Tier { get; init; }

    /// <summary>The highest number of pips earned in the division indicated by <see cref="Division" />.</summary>
    public required int Pips { get; init; }

    /// <summary>The highest amount of times the final division has been repeated.</summary>
    public required int Repeats { get; init; }
}
