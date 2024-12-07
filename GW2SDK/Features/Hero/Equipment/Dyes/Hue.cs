using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Dyes;

/// <summary>The colors by which dyes are grouped.</summary>
[PublicAPI]
[DefaultValue(Unspecified)]
[JsonConverter(typeof(HueJsonConverter))]
public enum Hue
{
    /// <summary>Dye remover.</summary>
    Unspecified,

    /// <summary>Shades of gray.</summary>
    Gray,

    /// <summary>Shades of brown.</summary>
    Brown,

    /// <summary>Shades of red.</summary>
    Red,

    /// <summary>Shades of orange.</summary>
    Orange,

    /// <summary>Shades of yellow.</summary>
    Yellow,

    /// <summary>Shades of green.</summary>
    Green,

    /// <summary>Shades of blue.</summary>
    Blue,

    /// <summary>Shades of purple.</summary>
    Purple
}
