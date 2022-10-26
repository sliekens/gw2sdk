using JetBrains.Annotations;

namespace GW2SDK.Guilds.Upgrades;

[PublicAPI]
public enum GuildUpgradeCostKind
{
    Item = 1,

    Collectible,

    Currency,

    Coins
}