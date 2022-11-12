using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
public sealed record MasteryReward : AchievementReward
{
    public required int Id { get; init; }

    public required MasteryRegionName Region { get; init; }
}
