using JetBrains.Annotations;

namespace GW2SDK.Guilds.Upgrades;

[PublicAPI]
public sealed record BankBag : GuildUpgrade
{
    public required int MaxItems { get; init; }

    public required int MaxCoins { get; init; }
}