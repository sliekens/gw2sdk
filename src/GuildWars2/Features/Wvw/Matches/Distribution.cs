namespace GuildWars2.Wvw.Matches;

/// <summary>A distribution of team scores in a World vs. World match.</summary>
[DataTransferObject]
public sealed record Distribution
{
    /// <summary>The amount of points the red team has.</summary>
    public required int Red { get; init; }

    /// <summary>The amount of points the blue team has.</summary>
    public required int Blue { get; init; }

    /// <summary>The amount of points the green team has.</summary>
    public required int Green { get; init; }
}
