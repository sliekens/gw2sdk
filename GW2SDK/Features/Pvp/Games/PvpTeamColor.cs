using System.Text.Json.Serialization;

namespace GuildWars2.Pvp.Games;

/// <summary>The PvP team colors.</summary>
[PublicAPI]
[JsonConverter(typeof(PvpTeamColorJsonConverter))]
public enum PvpTeamColor
{
    /// <summary>The red team.</summary>
    Red = 1,

    /// <summary>The blue team.</summary>
    Blue
}
