using System.ComponentModel;

namespace GuildWars2.Exploration.Maps;

/// <summary>The kind of maps in Guild Wars 2.</summary>
[PublicAPI]
[DefaultValue(Unknown)]
public enum MapKind
{
    /// <summary>An unknown map kind.</summary>
    Unknown,

    /// <summary>An open world map.</summary>
    Public,

    /// <summary>An instance map. Instance maps are maps that are only accessible to a single party or squad. Examples include
    /// dungeons, fractals, strike missions and raids.</summary>
    Instance,

    /// <summary>The Obsidian Sanctum jumping puzzle map (World vs. World).</summary>
    JumpPuzzle,

    /// <summary>The starter zone of a new character.</summary>
    Tutorial,

    /// <summary>A structured PvP map.</summary>
    Pvp,

    /// <summary>Eternal Battlegrounds (World vs. World).</summary>
    Center,

    /// <summary>Red Borderlands (World vs. World).</summary>
    RedHome,

    /// <summary>Blue Borderlands (World vs. World).</summary>
    BlueHome,

    /// <summary>Green Borderlands (World vs. World).</summary>
    GreenHome,

    /// <summary>Edge of the Mists (World vs. World).</summary>
    EdgeOfTheMists
}
