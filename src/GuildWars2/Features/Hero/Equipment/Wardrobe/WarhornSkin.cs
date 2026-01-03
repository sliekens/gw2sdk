using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a warhorn skin.</summary>
[JsonConverter(typeof(WarhornSkinJsonConverter))]
public sealed record WarhornSkin : WeaponSkin;
