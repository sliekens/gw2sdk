using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a Halloween consumable (some boosters).</summary>
[PublicAPI]
[JsonConverter(typeof(HalloweenConsumableJsonConverter))]
public sealed record HalloweenConsumable : Consumable;
