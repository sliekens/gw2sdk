using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Masteries;

/// <summary>The mastery regions.</summary>
[DefaultValue(Unknown)]
[JsonConverter(typeof(MasteryRegionNameJsonConverter))]
public enum MasteryRegionName
{
    /// <summary>Unknown mastery point region.</summary>
    Unknown,

    /// <summary>Core tyria masteries, red.</summary>
    Tyria,

    /// <summary>Heart of Thorns masteries, green.</summary>
    Maguuma,

    /// <summary>Path of Fire masteries, purple.</summary>
    Desert,

    /// <summary>Icebrood Saga masteries, blue.</summary>
    Tundra,

    /// <summary>End of Dragons masteries, turquoise.</summary>
    Jade,

    /// <summary>Secrets of the Obscure masteries, gold.</summary>
    Sky,

    /// <summary>Janthir Wilds masteries, dark blue.</summary>
    Wild,

    /// <summary>Visions of Eternity masteries, orange.</summary>
    Magic
}
