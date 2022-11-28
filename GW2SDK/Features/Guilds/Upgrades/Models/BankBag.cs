using JetBrains.Annotations;

namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
public sealed record BankBag : GuildUpgrade
{
    public required int MaxItems { get; init; }

    public required int MaxCoins { get; init; }
}
