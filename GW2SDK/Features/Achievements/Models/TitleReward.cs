using JetBrains.Annotations;

namespace GW2SDK.Achievements.Models;

[PublicAPI]
public sealed record TitleReward : AchievementReward
{
    public int Id { get; init; }
}
