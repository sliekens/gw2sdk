using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Logs;

/// <summary>The kinds of changes to guild influence.</summary>
[PublicAPI]
[JsonConverter(typeof(InfluenceActivityKindJsonConverter))]
public enum InfluenceActivityKind
{
    /// <summary>A guild member gifted influence to the guild.</summary>
    Gifted = 1,

    /// <summary>Influence was awarded to the guild by each character as it logs in. The guild's attendance log was only
    /// updated once per day.</summary>
    DailyLogin
}
