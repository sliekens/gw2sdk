namespace GuildWars2.Pvp.Standings;

/// <summary>Information about the current division standing of the player in a PvP League season.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record CurrentStanding
{
    /// <summary>The total number of pips earned in the season, minus any pips lost.</summary>
    public required int TotalPips { get; init; }

    /// <summary>The 0-based index of the player's current division in the season.</summary>
    public required int Division { get; init; }

    /// <summary>The 0-based index of the player's current tier in the divison indicated by <see cref="Division" />.</summary>
    public required int Tier { get; init; }

    /// <summary>The number of pips earned in the division indicated by <see cref="Division" />.</summary>
    public required int Pips { get; init; }

    /// <summary>The amount of times the final division has been repeated.</summary>
    public required int Repeats { get; init; }

    /// <summary>The current skill rating of the player.</summary>
    public required int? Rating { get; init; }

    /// <summary>The current skill rating decay of the player due to inactivity.</summary>
    public required int? Decay { get; init; }
}
