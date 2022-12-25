using System;
using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record LeaderboardEntry
{
    public required string Name { get; init; }

    public required string GuildId { get; init; }

    public required string TeamName { get; init; }

    public required int? TeamId { get; init; }

    public required int Rank { get; init; }

    public required DateTimeOffset Date { get; init; }

    public required IReadOnlyCollection<Score> Scores { get; init; }
}
