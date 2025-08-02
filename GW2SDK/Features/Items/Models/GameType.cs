using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>The game types where items can be used.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(GameTypeJsonConverter))]
public enum GameType
{
    /// <summary>No specific game type or unknown game type.</summary>
    None,

    /// <summary>Usable in activities.</summary>
    Activity,

    /// <summary>Usable in dungeons.</summary>
    Dungeon,

    /// <summary>Usable in open world PvE.</summary>
    Pve,

    /// <summary>Usable in PvP matches.</summary>
    Pvp,

    /// <summary>Usable in the Heart of the Mists.</summary>
    PvpLobby,

    /// <summary>Usable in World vs. World.</summary>
    Wvw
}
