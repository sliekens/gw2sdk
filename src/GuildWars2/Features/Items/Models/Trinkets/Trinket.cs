using System.Text.Json.Serialization;

using GuildWars2.Hero;

namespace GuildWars2.Items;

/// <summary>Information about a trinket. This type is the base type for all trinkets. Cast objects of this type to a more
/// specific type to access more properties.</summary>
[Inheritable]
[JsonConverter(typeof(TrinketJsonConverter))]
public record Trinket : Item, ICombatEquipment, IUpgradable
{
    /// <summary>The Attribute Adjustment factor. To calculate the final item stats of the item, multiply this value with an
    /// attribute's multiplier, then add the result to the attribute's base value.</summary>
    /// <remarks>The formula is: attribute_adjustment * multiplier + value.</remarks>
    public required double AttributeAdjustment { get; init; }

    /// <summary>The ID of the item's attribute combination, used for items with fixed stats. This property is not used for
    /// items with selectable stats.</summary>
    public required int? AttributeCombinationId { get; init; }

    /// <summary>The effective stats of the item.</summary>
    public required IDictionary<Extensible<AttributeName>, int> Attributes { get; init; }

    /// <summary>The effect which is applied to the player when the item is equipped.</summary>
    public required Buff? Buff { get; init; }

    /// <summary>The IDs of the attribute combinations that can be chosen for the item. This property is only used for items
    /// with selectable stats.</summary>
    public required IReadOnlyList<int> StatChoices { get; init; }

    /// <summary>The ID of the upgrade component in the upgrade slot, if any.</summary>
    public required int? SuffixItemId { get; init; }

#pragma warning disable CA1033 // Interface methods should be callable by child types
    int? IUpgradable.SecondarySuffixItemId => null;
#pragma warning restore CA1033 // Interface methods should be callable by child types

    /// <summary>The upgrade slots of the trinket.</summary>
    public IReadOnlyList<int?> UpgradeSlots =>
        this switch
        {
            _ when Flags.NotUpgradeable => [],
            _ when Rarity == Items.Rarity.Ascended => [],
            _ when Rarity == Items.Rarity.Legendary => [],
            _ => [SuffixItemId]
        };

    /// <summary>The number of upgrade slots available on the trinket.</summary>
    public virtual int UpgradeSlotCount =>
        this switch
        {
            _ when Flags.NotUpgradeable => 0,
            _ when Rarity == Items.Rarity.Ascended => 0,
            _ when Rarity == Items.Rarity.Legendary => 0,
            _ => 1
        };

    /// <summary>The infusion slots of the trinket (only available on ascended and legendary items).</summary>
    public required IReadOnlyList<InfusionSlot> InfusionSlots { get; init; }

    /// <summary>The number of infusion slots available on the trinket.</summary>
    public virtual int InfusionSlotCount => InfusionSlots.Count;
}
