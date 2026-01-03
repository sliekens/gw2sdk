using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a sword skin.</summary>
[JsonConverter(typeof(SwordSkinJsonConverter))]
public sealed record SwordSkin : WeaponSkin;
