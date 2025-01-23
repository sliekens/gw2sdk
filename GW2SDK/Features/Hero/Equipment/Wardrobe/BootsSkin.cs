using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a boots skin.</summary>
[PublicAPI]
[JsonConverter(typeof(BootsSkinJsonConverter))]
public sealed record BootsSkin : ArmorSkin;
