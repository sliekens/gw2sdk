using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Achievements.Rewards;

/// <summary>A coins reward for completing an achievement.</summary>
[PublicAPI]
[JsonConverter(typeof(CoinsRewardJsonConverter))]
public sealed record CoinsReward : AchievementReward
{
    /// <summary>The amount of coins awarded for completing the achievement.</summary>
    public required Coin Coins { get; init; }
}
