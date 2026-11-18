using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about a claimed guild hall.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record GuildHall : GuildUpgrade;
