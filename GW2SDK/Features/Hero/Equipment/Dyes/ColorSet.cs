using System.ComponentModel;

namespace GuildWars2.Hero.Equipment.Dyes;

/// <summary>The color sets by which dyes are grouped.</summary>
[PublicAPI]
[DefaultValue(Unspecified)]
public enum ColorSet
{
    /// <summary>Dye remover.</summary>
    Unspecified,

    /// <summary>Starter dyes, unlocked by default.</summary>
    Starter,

    /// <summary>Colors obtained from Common rarity dyes.</summary>
    Common,

    /// <summary>Colors obtained from Masterwork rarity dyes.</summary>
    Uncommon,

    /// <summary>Colors obtained from Rare dyes.</summary>
    Rare,

    /// <summary>Colors obtained from gem store dye kits.</summary>
    Exclusive
}