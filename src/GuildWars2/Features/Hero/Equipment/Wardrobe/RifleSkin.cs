using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a rifle skin.</summary>
[JsonConverter(typeof(RifleSkinJsonConverter))]
public sealed record RifleSkin : WeaponSkin;
