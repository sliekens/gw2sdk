using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a backpack skin.</summary>
[JsonConverter(typeof(BackpackSkinJsonConverter))]
public sealed record BackpackSkin : EquipmentSkin;
