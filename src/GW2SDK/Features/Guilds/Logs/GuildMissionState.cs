using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Logs;

/// <summary>The guild mission state transitions.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(GuildMissionStateJsonConverter))]
public enum GuildMissionState
{
    /// <summary>No specific state or unknown state.</summary>
    None,

    /// <summary>Logged when the mission starts.</summary>
    Start,

    /// <summary>Logged when the mission ends successfully.</summary>
    Success,

    /// <summary>Logged when the mission ends unsuccessfully.</summary>
    Fail
}
