using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a shortbow skin.</summary>
[PublicAPI]
[JsonConverter(typeof(ShortbowSkinJsonConverter))]
public sealed record ShortbowSkin : WeaponSkin;
