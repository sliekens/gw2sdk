using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information AxeSkinJsonConverterabout a dagger skin.</summary>
[JsonConverter(typeof(DaggerSkinJsonConverter))]
public sealed record DaggerSkin : WeaponSkin;
