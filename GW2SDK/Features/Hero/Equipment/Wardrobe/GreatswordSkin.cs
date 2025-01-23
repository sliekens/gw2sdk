using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a greatsword skin.</summary>
[PublicAPI]
[JsonConverter(typeof(GreatswordSkinJsonConverter))]
public sealed record GreatswordSkin : WeaponSkin;
