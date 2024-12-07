using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Teams;

/// <summary>The active state of a guild team.</summary>
[PublicAPI]
[JsonConverter(typeof(GuildTeamStateJsonConverter))]
public enum GuildTeamState
{
    /// <summary>Used when the team is active.</summary>
    Active = 1
}
