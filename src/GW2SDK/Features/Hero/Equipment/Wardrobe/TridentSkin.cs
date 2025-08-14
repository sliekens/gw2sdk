using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a trident skin.</summary>
[JsonConverter(typeof(TridentSkinJsonConverter))]
public sealed record TridentSkin : WeaponSkin;
