using System.Collections.Generic;
using GuildWars2.Annotations;
using JetBrains.Annotations;

namespace GuildWars2.Achievements.Dailies;

[PublicAPI]
[DataTransferObject]
public sealed record DailyAchievementGroup
{
    public required IReadOnlyCollection<DailyAchievement> Pve { get; init; }

    public required IReadOnlyCollection<DailyAchievement> Pvp { get; init; }

    public required IReadOnlyCollection<DailyAchievement> Wvw { get; init; }

    public required IReadOnlyCollection<DailyAchievement> Fractals { get; init; }

    public required IReadOnlyCollection<DailyAchievement> Special { get; init; }
}
