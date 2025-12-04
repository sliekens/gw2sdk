using System.ComponentModel;
using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Logs;

/// <summary>The kinds of changes to guild influence.</summary>
[DefaultValue(None)]
[JsonConverter(typeof(InfluenceActivityKindJsonConverter))]
public enum InfluenceActivityKind
{
    /// <summary>No specific activity or unknown activity.</summary>
    None,

    /// <summary>A guild member gifted influence to the guild.</summary>
    Gifted,

    /// <summary>Influence was awarded to the guild by each character as it logs in. The guild's attendance log was only
    /// updated once per day.</summary>
    DailyLogin
}
