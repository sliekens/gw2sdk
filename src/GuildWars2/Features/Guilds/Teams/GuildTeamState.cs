using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Teams;

/// <summary>The active state of a guild team.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(GuildTeamStateJsonConverter))]
public enum GuildTeamState
{
    /// <summary>No specific state or unknown state.</summary>
    None,

    /// <summary>Used when the team is active.</summary>
    Active
}
