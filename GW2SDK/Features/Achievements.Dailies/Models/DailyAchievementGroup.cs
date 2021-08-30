using System;
using GW2SDK.Annotations;
using JetBrains.Annotations;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record DailyAchievementGroup
    {
        public DailyAchievement[] Pve { get; init; } = Array.Empty<DailyAchievement>();

        public DailyAchievement[] Pvp { get; init; } = Array.Empty<DailyAchievement>();

        public DailyAchievement[] Wvw { get; init; } = Array.Empty<DailyAchievement>();

        public DailyAchievement[] Fractals { get; init; } = Array.Empty<DailyAchievement>();

        public DailyAchievement[] Special { get; init; } = Array.Empty<DailyAchievement>();
    }
}
