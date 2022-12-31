using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Matches.Scores;

[PublicAPI]
[DataTransferObject]
public sealed record MatchScores
{
    public required string Id { get; init; }

    public required Distribution Scores { get; init; }

    public required Distribution VictoryPoints { get; init; }

    public required IReadOnlyCollection<Skirmish> Skirmishes { get; init; }

    public required IReadOnlyCollection<MapSummary> Maps { get; init; }
}
