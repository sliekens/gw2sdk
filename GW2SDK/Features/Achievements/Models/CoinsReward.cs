using JetBrains.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed record CoinsReward : AchievementReward
    {
        public int Count { get; init; }
    }
}
