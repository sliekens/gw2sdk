using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Achievements
{
    [PublicAPI]
    public sealed class MasteryReward : AchievementReward
    {
        public int Id { get; set; }

        public MasteryRegionName Region { get; set; }
    }
}