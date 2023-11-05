namespace GuildWars2.Hero.Achievements;

/// <summary>A coins reward for completing an achievement.</summary>
[PublicAPI]
public sealed record CoinsReward : AchievementReward
{
    /// <summary>The amount of coins awarded for completing the achievement.</summary>
    public required Coin Coins { get; init; }
}
