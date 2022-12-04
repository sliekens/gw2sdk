using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Pvp.Seasons;

[PublicAPI]
[DataTransferObject]
public sealed record Leaderboard
{
    public required LeaderboardSetting Settings { get; init; }

    public required IReadOnlyCollection<LeaderboardScoring> Scorings { get; init; }
}
