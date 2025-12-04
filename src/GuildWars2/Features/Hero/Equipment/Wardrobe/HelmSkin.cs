using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a helm skin.</summary>
[JsonConverter(typeof(HelmSkinJsonConverter))]
public sealed record HelmSkin : ArmorSkin;
