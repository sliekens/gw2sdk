namespace GuildWars2.Hero.Achievements.Rewards;

/// <summary>A title reward for completing an achievement.</summary>
public sealed record TitleReward : AchievementReward
{
    /// <summary>The title ID of the reward.</summary>
    public required int Id { get; init; }
}
