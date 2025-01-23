using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a two-handed toy skin.</summary>
[PublicAPI]
[JsonConverter(typeof(ToyTwoHandedSkinJsonConverter))]
public sealed record ToyTwoHandedSkin : WeaponSkin;
