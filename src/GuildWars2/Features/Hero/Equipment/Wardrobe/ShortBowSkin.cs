using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a short bow skin.</summary>
[JsonConverter(typeof(ShortBowSkinJsonConverter))]
public sealed record ShortBowSkin : WeaponSkin;
