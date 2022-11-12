using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
public sealed record AchievementTextBit : AchievementBit
{
    public required string Text { get; init; }
}
