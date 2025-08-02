using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Pvp.Games;

/// <summary>The PvP team colors.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(PvpTeamColorJsonConverter))]
public enum PvpTeamColor
{
    /// <summary>No specific team or unknown team.</summary>
    None,

    /// <summary>The red team.</summary>
    Red,

    /// <summary>The blue team.</summary>
    Blue
}
