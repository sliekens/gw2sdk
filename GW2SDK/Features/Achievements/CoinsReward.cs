using JetBrains.Annotations;

namespace GW2SDK.Achievements;

[PublicAPI]
public sealed record CoinsReward : AchievementReward
{
    public required Coin Coins { get; init; }
}
