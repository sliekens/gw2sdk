using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a shoulders skin.</summary>
[PublicAPI]
[JsonConverter(typeof(ShouldersSkinJsonConverter))]
public sealed record ShouldersSkin : ArmorSkin;
