using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable item. This type is the base type for all consumable items. Cast objects of this
/// type to a more specific type to access more properties.</summary>
[Inheritable]
[JsonConverter(typeof(ConsumableJsonConverter))]
public record Consumable : Item;
