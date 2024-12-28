using GuildWars2.Hero;
using System.Text.Json.Serialization;

namespace GuildWars2.Items;

/// <summary>Information about an armor item. This type is the base type for all armor. Cast objects of this type to a more
/// specific type to access more properties.</summary>
[PublicAPI]
[Inheritable]
[JsonConverter(typeof(ArmorJsonConverter))]
public record Armor : Item, ICombatEquipment, IUpgradable
{
    /// <summary>The default skin ID for the armor. This skin can be unlocked in the wardrobe by binding the item.</summary>
    public required int DefaultSkinId { get; init; }

    /// <summary>The weight class of the armor.</summary>
    public required Extensible<WeightClass> WeightClass { get; init; }

    /// <summary>The defense rating of the armor. Defense adds together with Toughness to give the Armor attribute, which
    /// reduces incoming strike damage.</summary>
    public required int Defense { get; init; }

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

    /// <summary>The upgrade slots of the armor.</summary>
    public IReadOnlyList<int?> UpgradeSlots =>
        this switch
        {
            _ when Flags.NotUpgradeable => Empty.List<int?>(),
            _ => [SuffixItemId]
        };

    /// <summary>The number of upgrade slots available on the armor item.</summary>
    public virtual int UpgradeSlotCount =>
        this switch
        {
            _ when Flags.NotUpgradeable => 0,
            _ => 1
        };

    /// <summary>The infusion slots of the armor (only available on ascended and legendary items).</summary>
    public required IReadOnlyList<InfusionSlot> InfusionSlots { get; init; }

    /// <summary>The number of infusion slots available on the armor item.</summary>
    public virtual int InfusionSlotCount => InfusionSlots.Count;

    /// <summary>The IDs of the attribute combinations that can be chosen for the item. This property is only used for items
    /// with selectable stats.</summary>
    public required IReadOnlyList<int> StatChoices { get; init; }

    /// <inheritdoc />
    public virtual bool Equals(Armor? other)
    {
        return ReferenceEquals(this, other)
            || (base.Equals(other)
                && DefaultSkinId == other.DefaultSkinId
                && WeightClass == other.WeightClass
                && Defense == other.Defense
                && Math.Abs(AttributeAdjustment - other.AttributeAdjustment) < 0.001d
                && AttributeCombinationId == other.AttributeCombinationId
                && Buff == other.Buff
                && SuffixItemId == other.SuffixItemId
                && InfusionSlots.SequenceEqual(other.InfusionSlots)
                && Attributes.SequenceEqual(other.Attributes, AttributesComparer.Instance)
                && StatChoices.SequenceEqual(other.StatChoices));
    }

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(base.GetHashCode());
        hash.Add(DefaultSkinId);
        hash.Add(WeightClass);
        hash.Add(Defense);
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

        return hash.ToHashCode();
    }
}
