using JetBrains.Annotations;

namespace GW2SDK.Achievements.Models;

[PublicAPI]
public sealed record AchievementMinipetBit : AchievementBit
{
    public int Id { get; init; }
}