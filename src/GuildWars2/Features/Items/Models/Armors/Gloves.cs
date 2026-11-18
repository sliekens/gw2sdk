using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about hand armor.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Gloves : Armor;
