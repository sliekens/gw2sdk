using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a relic, which provides combat special effects when equipped.</summary>
[PublicAPI]
[JsonConverter(typeof(RelicJsonConverter))]
public sealed record Relic : Item;
