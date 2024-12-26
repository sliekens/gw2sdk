using GuildWars2.Hero;

namespace GuildWars2.Items;

/// <summary>The interface for items that can be equipped to increase the player's combat attributes.</summary>
public interface ICombatEquipment
{
    /// <summary>The Attribute Adjustment factor. To calculate the final item stats of the item, multiply this value with an
    /// attribute's multiplier, then add the result to the attribute's base value.</summary>
    /// <remarks>The formula is: attribute_adjustment * multiplier + value.</remarks>
    double AttributeAdjustment { get; }

    /// <summary>The ID of the item's attribute combination, used for items with fixed stats. This property is not used for
    /// items with selectable stats.</summary>
    int? AttributeCombinationId { get; }

    /// <summary>The IDs of the attribute combinations that can be chosen for the item. This property is only used for items
    /// with selectable stats.</summary>
    IReadOnlyList<int> StatChoices { get; }

    /// <summary>The effective stats of the item.</summary>
    IDictionary<Extensible<AttributeName>, int> Attributes { get; }

    /// <summary>The effect which is applied to the player when the item is equipped.</summary>
    Buff? Buff { get; }
}
