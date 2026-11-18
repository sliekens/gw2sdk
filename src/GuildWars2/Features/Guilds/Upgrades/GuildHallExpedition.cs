using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about a guild hall expedition, which grants a chance to claim that guild hall.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record GuildHallExpedition : GuildUpgrade;
