using GW2SDK.Annotations;

namespace GW2SDK.Achievements
{
    [PublicAPI]
    public sealed record AchievementTextBit : AchievementBit
    {
        public string Text { get; init; } = "";
    }
}
