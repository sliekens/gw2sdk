using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable that unlocks a glider skin.</summary>
[JsonConverter(typeof(GliderSkinUnlockerJsonConverter))]
public sealed record GliderSkinUnlocker : Unlocker;
