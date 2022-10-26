using JetBrains.Annotations;

namespace GW2SDK.Guilds.Upgrades;

[PublicAPI]
public sealed record BankBag : GuildUpgrade
{
    public int MaxItems { get; init; }

    public int MaxCoins { get; init; }
}