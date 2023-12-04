namespace GuildWars2.Guilds.Teams;

/// <summary>Information about a game's score.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Score
{
    /// <summary>The number of points for the red team.</summary>
    public required int Red { get; init; }

    /// <summary>The number of points for the blue team.</summary>
    public required int Blue { get; init; }
}
