using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Mumble;

/// <summary>The user interface states provided by the MumbleLink API as a flags enum.</summary>
[PublicAPI]
[Flags]
[DefaultValue(None)]
[JsonConverter(typeof(UiStateJsonConverter))]
public enum UiState : uint
{
    /// <summary>No flags are set.</summary>
    None,

    /// <summary>The world map is open.</summary>
    IsMapOpen = 0b_1,

    /// <summary>The compass is docked to the top right corner if set; otherwise, it is docked to the bottom right corner.</summary>
    IsCompassTopRight = 0b_10,

    /// <summary>The compass rotates with the camera if set; otherwise, it points north.</summary>
    DoesCompassHaveRotationEnabled = 0b_100,

    /// <summary>The game client currently has mouse focus.</summary>
    GameHasFocus = 0b_1000,

    /// <summary>The player is in a structured PvP game mode.</summary>
    IsInCompetitiveGameMode = 0b_1_0000,

    /// <summary>The cursor is in a text input field (e.g. chat, inventory search, destroy item confirmation etc.)</summary>
    /// <remarks>Does not work for the Trading Post text boxes.</remarks>
    TextboxHasFocus = 0b_10_0000,

    /// <summary>The player is in combat.</summary>
    IsInCombat = 0b_100_0000
}
