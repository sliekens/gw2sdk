using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class CoinsReward : AchievementReward
    {
        public int Count { get; set; }
    }
}
