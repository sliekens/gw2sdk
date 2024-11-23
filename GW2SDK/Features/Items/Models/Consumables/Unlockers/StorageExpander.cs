using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a storage expander, which increases the maximum stack size of all stored materials by 250
/// when consumed.</summary>
[PublicAPI]
[JsonConverter(typeof(StorageExpanderJsonConverter))]
public sealed record StorageExpander : Unlocker;
