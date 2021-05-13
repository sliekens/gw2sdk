using JetBrains.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed record AchievementTextBit : AchievementBit
    {
        public string Text { get; init; } = "";
    }
}
