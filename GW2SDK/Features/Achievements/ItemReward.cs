using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Achievements
{
    [PublicAPI]
    public sealed class ItemReward : AchievementReward
    {
        public int Id { get; set; }

        public int Count { get; set; }
    }
}