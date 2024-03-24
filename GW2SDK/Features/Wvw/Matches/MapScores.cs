using GuildWars2.Exploration.Maps;

namespace GuildWars2.Wvw.Matches;

/// <summary>Information about the scores of a map in a World vs. World match.</summary>
[PublicAPI]
[DataTransferObject]
public sealed record MapScores
{
    public required MapKind Kind { get; init; }

    public required Distribution Scores { get; init; }
}
