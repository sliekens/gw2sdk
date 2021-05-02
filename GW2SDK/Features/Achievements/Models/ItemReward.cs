using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed record ItemReward : AchievementReward
    {
        public int Id { get; init; }

        public int Count { get; init; }
    }
}
