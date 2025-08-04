using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a coat skin.</summary>
[JsonConverter(typeof(CoatSkinJsonConverter))]
public sealed record CoatSkin : ArmorSkin;
