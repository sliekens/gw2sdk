using GuildWars2.Exploration.Maps;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
[DataTransferObject]
public sealed record MapScores
{
    public required MapKind Kind { get; init; }

    public required Distribution Scores { get; init; }
}
