using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a Halloween consumable (some boosters).</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record HalloweenConsumable : Consumable;
