using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about leg armor.</summary>
[JsonConverter(typeof(LeggingsJsonConverter))]
public sealed record Leggings : Armor;
