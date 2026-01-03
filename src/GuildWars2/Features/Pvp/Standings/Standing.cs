namespace GuildWars2.Pvp.Standings;

/// <summary>Information about the player's pips and skill rating in a PvP League season.</summary>
[DataTransferObject]
public sealed record Standing
{
    /// <summary>The ID of the PvP League season.</summary>
    public required string SeasonId { get; init; }

    /// <summary>The current division of the player in the season.</summary>
    public required CurrentStanding Current { get; init; }

    /// <summary>The highest division the player achieved in the season.</summary>
    public required BestStanding Best { get; init; }
}
