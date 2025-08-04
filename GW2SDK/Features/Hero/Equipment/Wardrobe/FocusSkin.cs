using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a focus skin.</summary>
[JsonConverter(typeof(FocusSkinJsonConverter))]
public sealed record FocusSkin : WeaponSkin;
