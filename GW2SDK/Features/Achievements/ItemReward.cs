using JetBrains.Annotations;

namespace GuildWars2.Achievements;

[PublicAPI]
public sealed record ItemReward : AchievementReward
{
    public required int Id { get; init; }

    public required int Count { get; init; }
}
