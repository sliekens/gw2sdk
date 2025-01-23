using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a spear skin.</summary>
[PublicAPI]
[JsonConverter(typeof(SpearSkinJsonConverter))]
public sealed record SpearSkin : WeaponSkin;
