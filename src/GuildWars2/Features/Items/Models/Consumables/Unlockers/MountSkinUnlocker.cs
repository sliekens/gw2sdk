using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a mount skin consumable.</summary>
[JsonConverter(typeof(GuildWars2.ModelJsonConverterFactory))]
public sealed record MountSkinUnlocker : Unlocker;
