using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Dailies;

[PublicAPI]
[DataTransferObject]
public sealed record DailyAchievementGroup
{
    public IReadOnlyCollection<DailyAchievement> Pve { get; init; } =
        Array.Empty<DailyAchievement>();

    public IReadOnlyCollection<DailyAchievement> Pvp { get; init; } =
        Array.Empty<DailyAchievement>();

    public IReadOnlyCollection<DailyAchievement> Wvw { get; init; } =
        Array.Empty<DailyAchievement>();

    public IReadOnlyCollection<DailyAchievement> Fractals { get; init; } =
        Array.Empty<DailyAchievement>();

    public IReadOnlyCollection<DailyAchievement> Special { get; init; } =
        Array.Empty<DailyAchievement>();
}
