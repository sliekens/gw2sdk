using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a leggings skin.</summary>
[PublicAPI]
[JsonConverter(typeof(LeggingsSkinJsonConverter))]
public sealed record LeggingsSkin : ArmorSkin;
