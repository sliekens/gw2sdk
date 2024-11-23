using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about leg armor.</summary>
[PublicAPI]
[JsonConverter(typeof(LeggingsJsonConverter))]
public sealed record Leggings : Armor;
