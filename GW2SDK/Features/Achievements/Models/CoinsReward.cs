using JetBrains.Annotations;

namespace GW2SDK.Achievements.Models;

[PublicAPI]
public sealed record CoinsReward : AchievementReward
{
    public Coin Coins { get; init; }
}
