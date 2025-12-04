using System.Text.Json.Serialization;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about an armor skin.</summary>
[Inheritable]
[JsonConverter(typeof(ArmorSkinJsonConverter))]
public record ArmorSkin : EquipmentSkin
{
    /// <summary>The weight class which indicates which professions can wear this armor skin.</summary>
    public required Extensible<WeightClass> WeightClass { get; init; }

    /// <summary>Gets the default dyes for the armor skin, and any race-specific overrides.</summary>
    public required DyeSlotInfo? DyeSlots { get; init; }
}
