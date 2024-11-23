using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about shoulder armor.</summary>
[PublicAPI]
[JsonConverter(typeof(ShouldersJsonConverter))]
public sealed record Shoulders : Armor;
