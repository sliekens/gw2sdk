using GW2SDK.Annotations;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DataTransferObject]
    public sealed record DailyAchievementGroup
    {
        public DailyAchievement[] Pve { get; init; } = new DailyAchievement[0];

        public DailyAchievement[] Pvp { get; init; } = new DailyAchievement[0];

        public DailyAchievement[] Wvw { get; init; } = new DailyAchievement[0];

        public DailyAchievement[] Fractals { get; init; } = new DailyAchievement[0];

        public DailyAchievement[] Special { get; init; } = new DailyAchievement[0];
    }
}
