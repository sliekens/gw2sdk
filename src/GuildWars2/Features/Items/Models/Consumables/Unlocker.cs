using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable item that unlocks something for the account when consumed. This type is the
/// base type for all unlock items. Cast objects of this type to a more specific type to access more properties.</summary>
[JsonConverter(typeof(UnlockerJsonConverter))]
[Inheritable]
public record Unlocker : Consumable;
