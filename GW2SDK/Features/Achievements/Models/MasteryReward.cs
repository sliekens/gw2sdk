using JetBrains.Annotations;

namespace GW2SDK.Achievements.Models;

[PublicAPI]
public sealed record MasteryReward : AchievementReward
{
    public int Id { get; init; }

    public MasteryRegionName Region { get; init; }
}
