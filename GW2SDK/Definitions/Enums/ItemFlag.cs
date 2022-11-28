using JetBrains.Annotations;

namespace GuildWars2;

[PublicAPI]
public enum ItemFlag
{
    AccountBindOnUse = 1,

    AccountBound,

    Attuned,

    BulkConsume,

    DeleteWarning,

    HideSuffix,

    Infused,

    MonsterOnly,

    NoMysticForge,

    NoSalvage,

    NoSell,

    NoUnderwater,

    NotUpgradeable,

    SoulBindOnUse,

    SoulbindOnAcquire,

    Tonic,

    Unique
}
