using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about an amulet.</summary>
[JsonConverter(typeof(AmuletJsonConverter))]
public sealed record Amulet : Trinket;
