namespace GuildWars2.Pvp.Games;

/// <summary>Information about the score of a PvP game.</summary>
[DataTransferObject]
public sealed record Score
{
    /// <summary>The score of the red team.</summary>
    public required int Red { get; init; }

    /// <summary>The score of the blue team.</summary>
    public required int Blue { get; init; }
}
