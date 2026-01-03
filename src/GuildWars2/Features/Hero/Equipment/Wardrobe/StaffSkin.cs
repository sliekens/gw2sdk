using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a staff skin.</summary>
[JsonConverter(typeof(StaffSkinJsonConverter))]
public sealed record StaffSkin : WeaponSkin;
