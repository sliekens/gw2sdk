using System.Text.Json.Serialization;

using GuildWars2.Hero.Masteries;

namespace GuildWars2.Hero.Achievements.Rewards;

/// <summary>A mastery point reward for completing an achievement.</summary>
[PublicAPI]
[JsonConverter(typeof(MasteryPointRewardJsonConverter))]
public sealed record MasteryPointReward : AchievementReward
{
    /// <summary>The mastery point ID. This is not the same as the mastery ID.</summary>
    public required int Id { get; init; }

    /// <summary>The region to which this mastery point belongs. Affects the mastery point icon.</summary>
    public required Extensible<MasteryRegionName> Region { get; init; }
}
