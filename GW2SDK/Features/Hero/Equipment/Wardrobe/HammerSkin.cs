using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a hammer skin.</summary>
[JsonConverter(typeof(HammerSkinJsonConverter))]
public sealed record HammerSkin : WeaponSkin;
