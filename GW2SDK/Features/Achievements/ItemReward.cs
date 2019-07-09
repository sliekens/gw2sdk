using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class ItemReward : AchievementReward
    {
        public int Id { get; set; }

        public int Count { get; set; }
    }
}
