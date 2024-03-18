﻿using GuildWars2.Hero;

namespace GuildWars2.Items;

/// <summary>Information about an upgrade component.</summary>
[PublicAPI]
[Inheritable]
public record UpgradeComponent : Item
{
    /// <summary>Flags that indicate which types of items are compatible with the upgrade.</summary>
    public required UpgradeComponentFlags UpgradeComponentFlags { get; init; }

    /// <summary>Flags that indicate whether the upgrade is an infusion or enrichment.</summary>
    public required InfusionSlotFlags InfusionUpgradeFlags { get; init; }

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

    /// <summary>The suffix which is applied to the item name when this upgrade is applied.</summary>
    /// <remarks>The suffix is not applied to items when <see cref="ItemFlags.HideSuffix" /> is set.</remarks>
    public required string SuffixName { get; init; }
}
