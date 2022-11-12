using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
public sealed record ItemReward : AchievementReward
{
    public required int Id { get; init; }

    public required int Count { get; init; }
}
