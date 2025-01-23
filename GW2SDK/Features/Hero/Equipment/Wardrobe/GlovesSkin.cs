using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a gloves skin.</summary>
[PublicAPI]
[JsonConverter(typeof(GlovesSkinJsonConverter))]
public sealed record GlovesSkin : ArmorSkin;
