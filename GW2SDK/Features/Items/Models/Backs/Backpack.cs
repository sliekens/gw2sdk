using GuildWars2.Hero;

namespace GuildWars2.Items;

/// <summary>Information about a back item.</summary>
[PublicAPI]
public sealed record Backpack : Item
{
    /// <summary>The default skin ID for the back item. This skin can be unlocked in the wardrobe by binding the item.</summary>
    public required int DefaultSkinId { get; init; }

    /// <summary>The infusion slots of the back item (only available on ascended and legendary items).</summary>
    public required IReadOnlyList<InfusionSlot> InfusionSlots { get; init; }

    /// <summary>The Attribute Adjustment factor. To calculate the final item stats of the item, multiply this value with an
    /// attribute's multiplier, then add the result to the attribute's base value.</summary>
    /// <remarks>The formula is: attribute_adjustment * multiplier + value.</remarks>
    public required double AttributeAdjustment { get; init; }

    /// <summary>The ID of the item's attribute combination, used for items with fixed stats. This property is not used for
    /// items with selectable stats.</summary>
    public required int? AttributeCombinationId { get; init; }

    /// <summary>The effective stats of the item.</summary>
    public required IDictionary<AttributeName, int> Attributes { get; init; }

    /// <summary>The effect which is applied to the player when the item is equipped.</summary>
    public required Buff? Buff { get; init; }

    /// <summary>The ID of the upgrade component in the upgrade slot, if any.</summary>
    public required int? SuffixItemId { get; init; }

    /// <summary>The IDs of the attribute combinations that can be chosen for the item. This property is only used for items
    /// with selectable stats.</summary>
    public required IReadOnlyList<int> StatChoices { get; init; }

    /// <summary>If the current item can be infused, this collection contains the IDs of the infused variations of the current
    /// item. Each item in the collection represents a possible upgrade path.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesInto { get; init; }

    /// <summary>If the current item is infused, this collection contains the IDs of the non-infused variations.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesFrom { get; init; }
}
