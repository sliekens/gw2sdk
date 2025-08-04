using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable that unlocks a Jade Bot skin.</summary>
[JsonConverter(typeof(JadeBotSkinUnlockerJsonConverter))]
public sealed record JadeBotSkinUnlocker : Unlocker;
