namespace GuildWars2.Achievements;

[PublicAPI]
public sealed record MasteryPointReward : AchievementReward
{
    public required int Id { get; init; }

    public required MasteryRegionName Region { get; init; }
}
