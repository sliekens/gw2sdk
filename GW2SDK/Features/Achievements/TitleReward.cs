namespace GuildWars2.Achievements;

[PublicAPI]
public sealed record TitleReward : AchievementReward
{
    public required int Id { get; init; }
}
