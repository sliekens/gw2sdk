using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Dailies;

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
