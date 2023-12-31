using System.ComponentModel;

namespace GuildWars2.Hero.Equipment.Dyes;

/// <summary>The materials by which dyes are grouped.</summary>
[PublicAPI]
[DefaultValue(Unspecified)]
public enum Material
{
    /// <summary>Dye remover.</summary>
    Unspecified,

    /// <summary>Vibrant.</summary>
    Vibrant,

    /// <summary>Natural leather.</summary>
    Leather,

    /// <summary>Natural metallic.</summary>
    Metal
}
