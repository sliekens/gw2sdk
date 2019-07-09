using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class TitleReward : AchievementReward
    {
        public int Id { get; set; }
    }
}