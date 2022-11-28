using JetBrains.Annotations;

namespace GuildWars2.Guilds.Upgrades;

[PublicAPI]
public enum GuildUpgradeCostKind
{
    Item = 1,

    Collectible,

    Currency,

    Coins
}
