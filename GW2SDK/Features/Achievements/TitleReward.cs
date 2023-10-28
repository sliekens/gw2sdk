namespace GuildWars2.Achievements;

/// <summary>A title reward for completing an achievement.</summary>
[PublicAPI]
public sealed record TitleReward : AchievementReward
{
    /// <summary>The title ID of the reward.</summary>
    public required int Id { get; init; }
}
