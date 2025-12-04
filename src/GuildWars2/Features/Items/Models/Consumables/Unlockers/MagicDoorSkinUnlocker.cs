using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable that unlocks a magic door skin.</summary>
[JsonConverter(typeof(MagicDoorSkinUnlockerJsonConverter))]
public sealed record MagicDoorSkinUnlocker : Unlocker;
