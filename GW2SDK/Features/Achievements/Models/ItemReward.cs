using JetBrains.Annotations;

namespace GW2SDK.Achievements.Models;

[PublicAPI]
public sealed record ItemReward : AchievementReward
{
    public int Id { get; init; }

    public int Count { get; init; }
}