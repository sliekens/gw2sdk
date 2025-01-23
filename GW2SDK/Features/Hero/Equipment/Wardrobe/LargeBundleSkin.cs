using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a large bundle skin.</summary>
[PublicAPI]
[JsonConverter(typeof(LargeBundleSkinJsonConverter))]
public sealed record LargeBundleSkin : WeaponSkin;
