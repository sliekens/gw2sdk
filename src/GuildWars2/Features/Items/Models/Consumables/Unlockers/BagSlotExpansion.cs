using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a bag slot expansion, which adds an extra bag slot to a character's inventory when consumed.</summary>
[JsonConverter(typeof(BagSlotExpansionJsonConverter))]
public sealed record BagSlotExpansion : Unlocker;
