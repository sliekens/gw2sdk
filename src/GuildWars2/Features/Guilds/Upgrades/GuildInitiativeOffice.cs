using System.Text.Json.Serialization;

namespace GuildWars2.Guilds.Upgrades;

/// <summary>Information about access to the Guild Initiative's central office in Lion's Arch.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record GuildInitiativeOffice : GuildUpgrade;
