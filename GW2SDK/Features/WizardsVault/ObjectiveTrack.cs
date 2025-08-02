using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.WizardsVault;

/// <summary>The Wizard's Vault objective tracks.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(ObjectiveTrackJsonConverter))]
public enum ObjectiveTrack
{
    /// <summary>No specific track or unknown track.</summary>
    None,

    /// <summary>PvE objectives such as completing events.</summary>
    PvE,

    /// <summary>PvP objectives such as earning a top scoreboard stat.</summary>
    PvP,

    /// <summary>WvW objectives such as defeating a supply caravan.</summary>
    WvW
}
