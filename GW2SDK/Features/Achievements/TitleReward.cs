using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Achievements
{
    [PublicAPI]
    public sealed class TitleReward : AchievementReward
    {
        public int Id { get; set; }
    }
}