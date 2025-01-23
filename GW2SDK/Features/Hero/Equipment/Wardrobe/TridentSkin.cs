using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a trident skin.</summary>
[PublicAPI]
[JsonConverter(typeof(TridentSkinJsonConverter))]
public sealed record TridentSkin : WeaponSkin;
