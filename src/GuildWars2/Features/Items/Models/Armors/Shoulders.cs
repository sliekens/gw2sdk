using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about shoulder armor.</summary>
[JsonConverter(typeof(ShouldersJsonConverter))]
public sealed record Shoulders : Armor;
