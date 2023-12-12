using System.ComponentModel;

namespace GuildWars2.Hero.Masteries;

/// <summary>The mastery regions.</summary>
[PublicAPI]
[DefaultValue(Unknown)]
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

    /// <summary>End of Dragons masteries, blue.</summary>
    Jade,

    /// <summary>Secrets of the Obscure masteries, yellow.</summary>
    Sky
}
