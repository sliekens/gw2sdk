using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a beverage that causes intoxication when consumed.</summary>
[PublicAPI]
[JsonConverter(typeof(BoozeJsonConverter))]
public sealed record Booze : Consumable;
