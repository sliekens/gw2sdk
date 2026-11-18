using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about foot armor.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Boots : Armor;
