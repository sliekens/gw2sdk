using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a relic, which provides combat special effects when equipped.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Relic : Item;
