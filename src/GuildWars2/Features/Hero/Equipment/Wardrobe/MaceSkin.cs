using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a mace skin.</summary>
[JsonConverter(typeof(MaceSkinJsonConverter))]
public sealed record MaceSkin : WeaponSkin;
