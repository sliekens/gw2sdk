using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a harpoon gun skin.</summary>
[PublicAPI]
[JsonConverter(typeof(HarpoonGunSkinJsonConverter))]
public sealed record HarpoonGunSkin : WeaponSkin;
