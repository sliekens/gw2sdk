using GW2SDK.Infrastructure;

namespace GW2SDK.Features.Achievements
{
    public sealed class AchievementTextBit : AchievementBit
    {
        [NotNull]
        public string Text { get; set; }
    }
}