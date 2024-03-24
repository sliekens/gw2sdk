using System.ComponentModel;

namespace GuildWars2.Wvw;

/// <summary>The team colors in World vs. World.</summary>
[PublicAPI]
[DefaultValue(Neutral)]
public enum TeamColor
{
    /// <summary>The neutral color (white) for objectives that are not controlled by any team.</summary>
    Neutral,

    /// <summary>The blue team color for objectives controlled by the blue team.</summary>
    Blue,

    /// <summary>The red team color for objectives controlled by the red team.</summary>
    Red,

    /// <summary>The green team color for objectives controlled by the green team.</summary>
    Green
}
