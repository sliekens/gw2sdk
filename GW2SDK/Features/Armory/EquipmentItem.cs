using System;
using System.Collections.Generic;
using GW2SDK.Annotations;
using GW2SDK.ItemStats;
using JetBrains.Annotations;

namespace GW2SDK.Armory;

[PublicAPI]
[DataTransferObject]
public sealed record EquipmentItem
{
    /// <summary>The item ID.</summary>
    public required int Id { get; init; }

    /// <summary>The number of this item in the armory (i.e. not currently active in any slot).</summary>
    public required int? Count { get; init; }

    /// <summary>The slot where this item is equipped   .</summary>
    public required EquipmentSlot? Slot { get; init; }

    /// <summary>The item IDs of runes or sigils in this item.</summary>
    public required IReadOnlyCollection<int>? Upgrades { get; init; }

    /// <summary>The item IDs of infusions in this item.</summary>
    public required IReadOnlyCollection<int>? Infusions { get; init; }

    /// <summary>The skin ID.</summary>
    public required int? SkinId { get; init; }

    /// <summary>The attribute combination for items with selectable stats.</summary>
    public required SelectedStat? Stats { get; init; }

    /// <summary>Whether this item is bound.</summary>
    public required ItemBinding Binding { get; init; }

    /// <summary>The name of the character if the item is soulbound.</summary>
    public required string BoundTo { get; init; }

    /// <summary>Whether this item is currently equipped or stored in the (legendary) armory.</summary>
    public required EquipmentLocation Location { get; init; }

    /// <summary>The equipment tab numbers where this item is (re)used.</summary>
    public required IReadOnlyCollection<int>? Tabs { get; init; }

    // Always length 4
    /// <summary>The IDs of colors applied to the current item.</summary>
    public required IReadOnlyCollection<int?>? Dyes { get; init; }
}
