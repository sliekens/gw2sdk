using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Teams;

/// <summary>The roles that a guild PvP team member can have.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(GuildTeamRoleJsonConverter))]
public enum GuildTeamRole
{
    /// <summary>No specific role or unknown role.</summary>
    None,

    /// <summary>The captain of the team.</summary>
    Captain,

    /// <summary>A regular member of the team.</summary>
    Member
}
