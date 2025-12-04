using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Pvp.Games;

/// <summary>The result of a PvP match.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(PvpResultJsonConverter))]
public enum PvpResult
{
    /// <summary>No specific result or unknown result.</summary>
    None,

    /// <summary>Your team won.</summary>
    Victory,

    /// <summary>Your team lost.</summary>
    Defeat,

    /// <summary>The other team forfeited.</summary>
    Bye,

    /// <summary>Your team forfeited.</summary>
    Forfeit,

    /// <summary>You deserted your team.</summary>
    Desertion
}
