using JetBrains.Annotations;

namespace GW2SDK.Achievements.Models;

[PublicAPI]
public sealed record AchievementSkinBit : AchievementBit
{
    public int Id { get; init; }
}
