namespace GuildWars2.Achievements;

/// <summary>A mastery point reward for completing an achievement.</summary>
[PublicAPI]
public sealed record MasteryPointReward : AchievementReward
{
    /// <summary>The mastery point ID. This is not the same as the mastery ID.</summary>
    public required int Id { get; init; }

    /// <summary>The region to which this mastery point belongs. Affects the mastery point icon.</summary>
    public required MasteryRegionName Region { get; init; }
}
