using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about an aquatic helm skin.</summary>
[PublicAPI]
[JsonConverter(typeof(HelmAquaticSkinJsonConverter))]
public sealed record HelmAquaticSkin : ArmorSkin;
