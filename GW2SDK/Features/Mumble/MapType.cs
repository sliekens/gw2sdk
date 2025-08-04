using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Mumble;

#pragma warning disable CA1028 // Enum Storage should be Int32

/// <summary>The map types.</summary>
[DefaultValue(AutoRedirect)]
[JsonConverter(typeof(MapTypeJsonConverter))]
[CLSCompliant(false)]
public enum MapType : uint
{
    // Keep the enum values in sync with the enum from the game, provided below.
    // The name can be different, but the values must match.
    // Unused values are commented out to avoid confusing the user.
    //
    // enum EMapType
    // {
    //     MAP_TYPE_AUTO_REDIRECT,
    //     MAP_TYPE_CHAR_CREATE,
    //     MAP_TYPE_COMPETITIVE_PVP,
    //     MAP_TYPE_GVG,
    //     MAP_TYPE_INSTANCE,
    //     MAP_TYPE_PUBLIC,
    //     MAP_TYPE_TOURNAMENT,
    //     MAP_TYPE_TUTORIAL,
    //     MAP_TYPE_USERTOURN,
    //     MAP_TYPE_WVW_CENTER,
    //     MAP_TYPE_WVW_BLUE_HOME,
    //     MAP_TYPE_WVW_GREEN_HOME,
    //     MAP_TYPE_WVW_RED_HOME,
    //     MAP_TYPE_WVW_REWARD,
    //     MAP_TYPE_WVW_EB_JUMP_PUZZLE,
    //     MAP_TYPE_WVW_OVERFLOW,
    //     MAP_TYPE_PUBLIC_MINI,
    //     MAP_TYPE_BIG_BATTLE, // NO LONGER SUPPORTED
    //     MAP_TYPE_WVW_LOUNGE,
    //     MAP_TYPE_WVW,
    //     MAP_TYPES
    // };

    /// <summary>Redirect (e.g., when you log in while in a PvP match).</summary>
    AutoRedirect,

    //CharacterCreation = 1,

    /// <summary>A structured PvP map.</summary>
    CompetitivePvP = 2,

    //GvG = 3,

    /// <summary>An instance map. Instance maps are maps that are only accessible to a single party or squad. Examples include
    /// dungeons, fractals, strike missions and raids.</summary>
    Instance = 4,

    /// <summary>Large open world maps.</summary>
    Public = 5,

    //Tournament = 6,

    /// <summary>The starter zone of a new character.</summary>
    Tutorial = 7,

    //UserTournament = 8,

    /// <summary>Eternal Battlegrounds (World vs. World).</summary>
    Center = 9,

    /// <summary>Blue Borderlands (World vs. World).</summary>
    BlueHome = 10,

    /// <summary>Green Borderlands (World vs. World).</summary>
    GreenHome = 11,

    /// <summary>Red Borderlands (World vs. World).</summary>
    RedHome = 12,

    //FortunesVale = 13,

    /// <summary>The Obsidian Sanctum jumping puzzle map (World vs. World).</summary>
    ObsidianSanctum = 14,

    /// <summary>Edge of the Mists (World vs. World).</summary>
    EdgeOfTheMists = 15,

    /// <summary>Small open world maps, like Arborstone.</summary>
    PublicMini = 16,

    //BigBattle = 17,

    /// <summary>Armistice Bastion.</summary>
    WvwLounge = 18

    //Wvw = 19
}
