using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about head armor.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Helm : Armor;
