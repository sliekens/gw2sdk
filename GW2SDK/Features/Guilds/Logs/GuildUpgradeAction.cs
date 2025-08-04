using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Logs;

/// <summary>The guild upgrade actions.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(GuildUpgradeActionJsonConverter))]
public enum GuildUpgradeAction
{
    /// <summary>No specific action or unknown action.</summary>
    None,

    /// <summary>The upgrade was queued for processing.</summary>
    Queued,

    /// <summary>The upgrade was cancelled.</summary>
    Cancelled,

    /// <summary>The upgrade was completed.</summary>
    Completed,

    /// <summary>The upgrade was sped up.</summary>
    SpedUp
}
