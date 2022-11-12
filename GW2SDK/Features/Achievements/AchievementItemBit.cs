using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
public sealed record AchievementItemBit : AchievementBit
{
    public required int Id { get; init; }
}
