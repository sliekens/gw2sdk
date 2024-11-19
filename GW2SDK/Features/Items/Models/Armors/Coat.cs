using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about chest armor.</summary>
[PublicAPI]
[JsonConverter(typeof(CoatJsonConverter))]
public sealed record Coat : Armor;
