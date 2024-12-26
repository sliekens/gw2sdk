namespace GuildWars2.Items;

/// <summary>The interface for items with upgrade or infusion slots.</summary>
[PublicAPI]
public interface IUpgradable
{
    /// <summary>The ID of the upgrade component in the upgrade slot, if any.</summary>
    int? SuffixItemId { get; }

    /// <summary>The ID of the upgrade component in the second upgrade slot (two-handed weapons only), if any.</summary>
    int? SecondarySuffixItemId { get; }

    /// <summary>The infusion slots of the item (only available on ascended and legendary items).</summary>
    IReadOnlyList<InfusionSlot> InfusionSlots { get; }
}
