using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable that unlocks a random skin or dye, for example Guaranteed Wardrobe Unlock or
/// Guaranteed Dye Unlock.</summary>
[JsonConverter(typeof(RandomUnlockerJsonConverter))]
public sealed record RandomUnlocker : Consumable;
