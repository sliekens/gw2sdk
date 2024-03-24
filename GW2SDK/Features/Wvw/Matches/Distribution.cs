namespace GuildWars2.Wvw.Matches;

/// <summary>A distribution of team scores in a World vs. World match.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record Distribution
{
    /// <summary>The amount of points the red team has.</summary>
    public required int Red { get; init; }

    /// The amount of points the blue team has.
    public required int Blue { get; init; }

    /// The amount of points the green team has.
    public required int Green { get; init; }
}
