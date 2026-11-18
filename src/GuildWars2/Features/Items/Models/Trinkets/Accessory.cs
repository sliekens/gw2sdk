using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about an accessory.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Accessory : Trinket;
