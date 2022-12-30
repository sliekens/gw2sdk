using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Wvw.Matches;

[PublicAPI]
[DataTransferObject]
public sealed record Skirmish
{
    public required int Id { get; init; }

    public required Distribution Scores { get; init; }

    public required IReadOnlyCollection<MapScores> MapScores { get; init; }
}
