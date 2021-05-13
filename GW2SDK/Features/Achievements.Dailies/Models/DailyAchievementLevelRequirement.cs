using GW2SDK.Annotations;

namespace GW2SDK.Achievements.Dailies
{
    [PublicAPI]
    [DataTransferObject(RootObject = false)]
    public sealed record DailyAchievementLevelRequirement
    {
        public int Min { get; init; }

        public int Max { get; init; }
    }
}
