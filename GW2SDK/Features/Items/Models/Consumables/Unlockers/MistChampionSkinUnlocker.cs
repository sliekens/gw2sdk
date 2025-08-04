using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable that unlocks a mist champion skin.</summary>
[JsonConverter(typeof(MistChampionSkinUnlockerJsonConverter))]
public sealed record MistChampionSkinUnlocker : Unlocker;
