using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Achievements
{
    [PublicAPI]
    public sealed class CoinsReward : AchievementReward
    {
        public int Count { get; set; }
    }
}