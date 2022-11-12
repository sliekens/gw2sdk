using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
public sealed record AchievementMinipetBit : AchievementBit
{
    public required int Id { get; init; }
}
