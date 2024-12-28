using System.Text.Json.Serialization;
using GuildWars2.Hero;

namespace GuildWars2.Items;

/// <summary>Information about a back item.</summary>
[PublicAPI]
[JsonConverter(typeof(BackpackJsonConverter))]
public sealed record Backpack : Item, ICombatEquipment, IUpgradable, IInfused, IInfusable
{
    /// <summary>The default skin ID for the back item. This skin can be unlocked in the wardrobe by binding the item.</summary>
    public required int DefaultSkinId { get; init; }

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

    /// <summary>The ID of the upgrade component in the upgrade slot, if any.</summary>
    public required int? SuffixItemId { get; init; }

    int? IUpgradable.SecondarySuffixItemId => null;

    /// <summary>The upgrade slots of the back item.</summary>
    public IReadOnlyList<int?> UpgradeSlots =>
        this switch
        {
            _ when Flags.NotUpgradeable => [],
            _ when Rarity == Items.Rarity.Ascended => [],
            _ when Rarity == Items.Rarity.Legendary => [],
            _ => [SuffixItemId]
        };

    /// <summary>The number of upgrade slots available on the back item.</summary>
    public int UpgradeSlotCount =>
        this switch
        {
            _ when Flags.NotUpgradeable => 0,
            _ when Rarity == Items.Rarity.Ascended => 0,
            _ when Rarity == Items.Rarity.Legendary => 0,
            _ => 1
        };

    /// <summary>The infusion slots of the back item (only available on ascended and legendary items).</summary>
    public required IReadOnlyList<InfusionSlot> InfusionSlots { get; init; }

    /// <summary>The number of infusion slots available on the back item.</summary>
    public int InfusionSlotCount => InfusionSlots.Count;

    /// <summary>The IDs of the attribute combinations that can be chosen for the item. This property is only used for items
    /// with selectable stats.</summary>
    public required IReadOnlyList<int> StatChoices { get; init; }

    /// <summary>If the current back item can be infused in the Mystic Forge, this collection contains the IDs of the infused variations of the
    /// back item. Each item in the collection represents a different recipe.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradePath> UpgradesInto { get; init; }

    /// <summary>If the current back item is infused, this collection contains the IDs of possible source items.</summary>
    public required IReadOnlyCollection<InfusionSlotUpgradeSource> UpgradesFrom { get; init; }

    /// <inheritdoc />
    public bool Equals(Backpack? other)
    {
        return ReferenceEquals(this, other)
            || (base.Equals(other)
                && DefaultSkinId == other.DefaultSkinId
                && Math.Abs(AttributeAdjustment - other.AttributeAdjustment) < 0.001d
                && AttributeCombinationId == other.AttributeCombinationId
                && Buff == other.Buff
                && SuffixItemId == other.SuffixItemId
                && InfusionSlots.SequenceEqual(other.InfusionSlots)
                && Attributes.SequenceEqual(other.Attributes, AttributesComparer.Instance)
                && StatChoices.SequenceEqual(other.StatChoices)
                && UpgradesInto.SequenceEqual(other.UpgradesInto)
                && UpgradesFrom.SequenceEqual(other.UpgradesFrom));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(base.GetHashCode());
        hash.Add(DefaultSkinId);
        hash.Add(AttributeAdjustment);
        hash.Add(AttributeCombinationId);
        hash.Add(Buff);
        hash.Add(SuffixItemId);
        foreach (var slot in InfusionSlots)
        {
            hash.Add(slot);
        }

        foreach (var attribute in Attributes)
        {
            hash.Add(attribute.Key);
            hash.Add(attribute.Value);
        }

        foreach (var statChoice in StatChoices)
        {
            hash.Add(statChoice);
        }

        foreach (var upgrade in UpgradesInto)
        {
            hash.Add(upgrade);
        }

        foreach (var source in UpgradesFrom)
        {
            hash.Add(source);
        }

        return hash.ToHashCode();
    }
}
