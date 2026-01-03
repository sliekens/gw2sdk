using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about a consumable that unlocks a Conjured Doorway skin.</summary>
[JsonConverter(typeof(ConjuredDoorwayUnlockerJsonConverter))]
public sealed record ConjuredDoorwayUnlocker : Unlocker;
