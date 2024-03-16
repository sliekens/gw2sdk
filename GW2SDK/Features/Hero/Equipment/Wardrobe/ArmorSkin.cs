using GuildWars2.Items;

namespace GuildWars2.Hero.Equipment.Wardrobe;

/// <summary>Information about an armor skin.</summary>
[PublicAPI]
[Inheritable]
public record ArmorSkin : EquipmentSkin
{
    /// <summary>The weight class which indicates which professions can wear this armor skin.</summary>
    public required WeightClass WeightClass { get; init; }

    /// <summary>Gets the default dyes for the armor skin, and any race-specific overrides.</summary>
    public required DyeSlotInfo? DyeSlots { get; init; }
}
