using GW2SDK.Annotations;
using GW2SDK.Enums;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class MasteryReward : AchievementReward
    {
        public int Id { get; set; }

        public MasteryRegionName Region { get; set; }
    }
}
