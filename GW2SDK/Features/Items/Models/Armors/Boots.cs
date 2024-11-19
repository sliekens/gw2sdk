using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about foot armor.</summary>
[PublicAPI]
[JsonConverter(typeof(BootsJsonConverter))]
public sealed record Boots : Armor;
