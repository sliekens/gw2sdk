using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed class AchievementTextBit : AchievementBit
    {
        [NotNull]
        public string Text { get; set; }
    }
}
