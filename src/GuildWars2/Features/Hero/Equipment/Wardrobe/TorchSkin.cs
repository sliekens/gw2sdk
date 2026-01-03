using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a torch skin.</summary>
[JsonConverter(typeof(TorchSkinJsonConverter))]
public sealed record TorchSkin : WeaponSkin;
