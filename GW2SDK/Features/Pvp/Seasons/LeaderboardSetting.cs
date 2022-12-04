using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record LeaderboardSetting
{
    public required string Name { get; init; }

    public required string ScoringId { get; init; }

    public required IReadOnlyCollection<LeaderboardTier> Tiers { get; init; }
}
