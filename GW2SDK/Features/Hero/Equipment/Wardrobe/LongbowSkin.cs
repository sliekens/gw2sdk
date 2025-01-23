using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a longbow skin.</summary>
[PublicAPI]
[JsonConverter(typeof(LongbowSkinJsonConverter))]
public sealed record LongbowSkin : WeaponSkin;
