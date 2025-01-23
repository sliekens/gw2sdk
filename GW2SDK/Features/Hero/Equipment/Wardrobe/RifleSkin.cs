using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a rifle skin.</summary>
[PublicAPI]
[JsonConverter(typeof(RifleSkinJsonConverter))]
public sealed record RifleSkin : WeaponSkin;
