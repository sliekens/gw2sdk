using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a shared inventory slot, which adds an extra shared inventory slot to every character on the
/// account when consumed.</summary>
[PublicAPI]
[JsonConverter(typeof(SharedInventorySlotJsonConverter))]
public sealed record SharedInventorySlot : Unlocker;
