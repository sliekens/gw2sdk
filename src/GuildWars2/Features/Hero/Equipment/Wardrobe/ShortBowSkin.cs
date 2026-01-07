using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a shortbow skin.</summary>
[JsonConverter(typeof(ShortBowSkinJsonConverter))]
public sealed record ShortBowSkin : WeaponSkin;
