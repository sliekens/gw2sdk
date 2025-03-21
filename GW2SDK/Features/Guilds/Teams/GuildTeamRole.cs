﻿using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Teams;

/// <summary>The roles that a guild PvP team member can have.</summary>
[PublicAPI]
[JsonConverter(typeof(GuildTeamRoleJsonConverter))]
public enum GuildTeamRole
{
    /// <summary>The captain of the team.</summary>
    Captain = 1,

    /// <summary>A regular member of the team.</summary>
    Member
}
