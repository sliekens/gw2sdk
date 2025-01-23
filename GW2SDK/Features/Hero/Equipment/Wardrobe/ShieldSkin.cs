using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a shield skin.</summary>
[PublicAPI]
[JsonConverter(typeof(ShieldSkinJsonConverter))]
public sealed record ShieldSkin : WeaponSkin;
