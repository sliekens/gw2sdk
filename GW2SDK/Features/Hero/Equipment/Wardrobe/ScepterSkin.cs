using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a scepter skin.</summary>
[PublicAPI]
[JsonConverter(typeof(ScepterSkinJsonConverter))]
public sealed record ScepterSkin : WeaponSkin;
