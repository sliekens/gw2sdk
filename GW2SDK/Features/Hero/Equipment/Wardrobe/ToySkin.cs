using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a toy skin.</summary>
[PublicAPI]
[JsonConverter(typeof(ToySkinJsonConverter))]
public sealed record ToySkin : WeaponSkin;
