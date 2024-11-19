using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about an outfit consumable.</summary>
[PublicAPI]
[JsonConverter(typeof(OutfitUnlockerJsonConverter))]
public sealed record OutfitUnlocker : Unlocker;
