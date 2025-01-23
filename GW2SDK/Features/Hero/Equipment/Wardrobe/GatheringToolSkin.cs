using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about a gathering tool skin.</summary>
[PublicAPI]
[Inheritable]
[JsonConverter(typeof(GatheringToolSkinJsonConverter))]
public record GatheringToolSkin : EquipmentSkin;
