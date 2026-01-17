namespace GuildWars2.Items;

/// <summary>The interface for items with upgrade or infusion slots.</summary>
public interface IUpgradable
{
    /// <summary>The ID of the upgrade component in the upgrade slot, if any.</summary>
    int? SuffixItemId { get; }

    /// <summary>The ID of the upgrade component in the second upgrade slot (two-handed weapons only), if any.</summary>
    int? SecondarySuffixItemId { get; }

    /// <summary>The upgrade slots of the item.</summary>
    IImmutableValueList<int?> UpgradeSlots { get; }

    /// <summary>The number of upgrade slots available on the item.</summary>
    int UpgradeSlotCount { get; }

    /// <summary>The infusion slots of the item (only available on ascended and legendary items).</summary>
    IImmutableValueList<InfusionSlot> InfusionSlots { get; }

    /// <summary>The number of infusion slots available on the item.</summary>
    int InfusionSlotCount { get; }
}
