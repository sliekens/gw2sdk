using JetBrains.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed record TitleReward : AchievementReward
    {
        public int Id { get; init; }
    }
}
