using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a gathering tool. This type is the base type for all gathering tools. Cast objects of this
/// type to a more specific type to access more properties.</summary>
[PublicAPI]
[Inheritable]
[JsonConverter(typeof(GatheringToolJsonConverter))]
public record GatheringTool : Item;
