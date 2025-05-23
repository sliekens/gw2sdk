using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Pvp.Games;

/// <summary>The PvP game rating types. Player skill is rated differently for each game type.</summary>
[PublicAPI]
[DefaultValue(None)]
[JsonConverter(typeof(PvpRatingTypeJsonConverter))]
public enum PvpRatingType
{
    /// <summary>An unrated game.</summary>
    None,

    /// <summary>A ranked game with 5 players per team.</summary>
    Ranked,

    /// <summary>A ranked game with 2 players per team.</summary>
    Ranked2v2,

    /// <summary>A ranked game with 3 players per team.</summary>
    Ranked3v3,

    /// <summary>An unranked game.</summary>
    Unranked,

    /// <summary>A placement game for a new PvP season.</summary>
    /// <remarks>The player skill rating is reset at the beginning of each season and 10 placement rounds must be played to
    /// obtain a new rating.</remarks>
    Placeholder,

    /// <summary>A ranked Push game.</summary>
    RankedPush,
}
