using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Pve.SuperAdventureBox;

/// <summary>The difficulty modes of Super Adventure Box zones.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(SuperAdventureBoxModeJsonConverter))]
public enum SuperAdventureBoxMode
{
    /// <summary>No specific mode or unknown mode.</summary>
    None,

    /// <summary>The easiest difficulty mode.</summary>
    Exploration,

    /// <summary>The normal difficulty mode.</summary>
    Normal,

    /// <summary>The hardest difficulty mode.</summary>
    Tribulation
}
