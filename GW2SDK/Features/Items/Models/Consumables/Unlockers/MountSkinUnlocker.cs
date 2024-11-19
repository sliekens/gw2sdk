using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a mount skin consumable.</summary>
[PublicAPI]
[JsonConverter(typeof(MountSkinUnlockerJsonConverter))]
public sealed record MountSkinUnlocker : Unlocker;
