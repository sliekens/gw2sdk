using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a pistol skin.</summary>
[JsonConverter(typeof(PistolSkinJsonConverter))]
public sealed record PistolSkin : WeaponSkin;
