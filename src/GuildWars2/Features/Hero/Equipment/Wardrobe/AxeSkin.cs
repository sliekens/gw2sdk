using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about an axe skin.</summary>
[JsonConverter(typeof(AxeSkinJsonConverter))]
public sealed record AxeSkin : WeaponSkin;
