using JetBrains.Annotations;

namespace GuildWars2.Achievements;

[PublicAPI]
public sealed record CoinsReward : AchievementReward
{
    public required Coin Coins { get; init; }
}
