using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about an upgrade extractor, which is used to remove upgrade components from equipment.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record UpgradeExtractor : Consumable;
