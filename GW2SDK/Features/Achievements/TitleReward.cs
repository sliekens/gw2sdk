using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
public sealed record TitleReward : AchievementReward
{
    public required int Id { get; init; }
}
