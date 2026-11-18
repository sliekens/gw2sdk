using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a beverage that causes intoxication when consumed.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record Booze : Consumable;
