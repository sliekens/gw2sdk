using System.Text.Json.Serialization;

namespace GuildWars2.Pve.SuperAdventureBox;

/// <summary>The difficulty modes of Super Adventure Box zones.</summary>
[PublicAPI]
[JsonConverter(typeof(SuperAdventureBoxModeJsonConverter))]
public enum SuperAdventureBoxMode
{
    /// <summary>The easiest difficulty mode.</summary>
    Exploration = 1,

    /// <summary>The normal difficulty mode.</summary>
    Normal,

    /// <summary>The hardest difficulty mode.</summary>
    Tribulation
}
