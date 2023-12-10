using System.ComponentModel;

namespace GuildWars2.Exploration.Maps;

/// <summary>The kind of maps in Guild Wars 2.</summary>
[PublicAPI]
[DefaultValue(Unknown)]
public enum MapKind
{
    /// <summary>An unknown map kind, probably only used for new maps.</summary>
    Unknown,

    /// <summary>An open world map.</summary>
    Public,

    /// <summary>An instance map. Instance maps are maps that are only accessible to a single party or squad. Examples include
    /// dungeons, fractals, strike missions and raids.</summary>
    Instance,

    /// <summary>A jump puzzle map.</summary>
    JumpPuzzle,

    /// <summary>The starter zone of a new character.</summary>
    Tutorial,

    /// <summary>A structured PvP map.</summary>
    Pvp,

    /// <summary>(WvW) Eternal Battlegrounds.</summary>
    Center,

    /// <summary>(WvW) Red Borderlands.</summary>
    RedHome,

    /// <summary>(WvW) Blue Borderlands.</summary>
    BlueHome,

    /// <summary>(WvW) Green Borderlands.</summary>
    GreenHome,

    /// <summary>(WvW) Edge of the Mists.</summary>
    EdgeOfTheMists
}
