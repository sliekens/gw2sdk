using System.Text.Json.Serialization;
using GuildWars2.Hero;

namespace GuildWars2.Items;

/// <summary>Information about a weapon item. This type is the base type for all weapon. Cast objects of this type to a
/// more specific type to access more properties.</summary>
[PublicAPI]
[Inheritable]
[JsonConverter(typeof(WeaponJsonConverter))]
public record Weapon : Item, ICombatEquipment, IUpgradable
{
    /// <summary>The default skin ID for the weapon. This skin can be unlocked in the wardrobe by binding the item.</summary>
    public required int DefaultSkinId { get; init; }

    /// <summary>The type of damage dealt by the weapon. It is a purely visual effect and does not affect the damage
    /// calculation.</summary>
    public required Extensible<DamageType> DamageType { get; init; }

    /// <summary>The minimum power used to calculate strike damage. Strike damage is calculated based a random number between
    /// <see cref="MinPower" /> and <see cref="MaxPower" />.</summary>
    public required int MinPower { get; init; }

    /// <summary>The maximum power used to calculate strike damage. Strike damage is calculated based a random number between
    /// <see cref="MinPower" /> and <see cref="MaxPower" />.</summary>
    public required int MaxPower { get; init; }

    /// <summary>The defense rating of the weapon (shields only). Defense adds together with Toughness to give the Armor
    /// attribute, which reduces incoming strike damage.</summary>
    public required int Defense { get; init; }

    /// <summary>Indicates whether the weapon is two-handed and can have a secondary suffix item.</summary>
    public virtual bool TwoHanded => false;

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

    /// <inheritdoc />
    public virtual bool Equals(Weapon? other)
    {
        return ReferenceEquals(this, other)
            || (base.Equals(other)
                && DefaultSkinId == other.DefaultSkinId
                && DamageType.Equals(other.DamageType)
                && MinPower == other.MinPower
                && MaxPower == other.MaxPower
                && Defense == other.Defense
                && Math.Abs(AttributeAdjustment - other.AttributeAdjustment) < 0.001d
                && AttributeCombinationId == other.AttributeCombinationId
                && Buff == other.Buff
                && SuffixItemId == other.SuffixItemId
                && SecondarySuffixItemId == other.SecondarySuffixItemId
                && Attributes.SequenceEqual(other.Attributes, AttributesComparer.Instance)
                && InfusionSlots.SequenceEqual(other.InfusionSlots)
                && StatChoices.SequenceEqual(other.StatChoices));
    }

    /// <summary>The ID of the upgrade component in the upgrade slot, if any.</summary>
    public required int? SuffixItemId { get; init; }

    /// <summary>The ID of the upgrade component in the second upgrade slot (two-handed weapons only), if any.</summary>
    public required int? SecondarySuffixItemId { get; init; }

    /// <summary>The upgrade slots of the weapon.</summary>
    public IReadOnlyList<int?> UpgradeSlots =>
        this switch
        {
            _ when Flags.NotUpgradeable => [],
            _ when !TwoHanded => [SuffixItemId],
            _ => [SuffixItemId, SecondarySuffixItemId]
        };

    /// <summary>The number of upgrade slots available on the weapon.</summary>
    public virtual int UpgradeSlotCount =>
        this switch
        {
            _ when Flags.NotUpgradeable => 0,
            _ when TwoHanded => 2,
            _ => 1
        };

    /// <summary>The infusion slots of the weapon (only available on ascended and legendary items).</summary>
    public required IReadOnlyList<InfusionSlot> InfusionSlots { get; init; }

    /// <summary>The number of infusion slots available on the weapon.</summary>
    public virtual int InfusionSlotCount => InfusionSlots.Count;

    /// <inheritdoc />
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(base.GetHashCode());
        hash.Add(DefaultSkinId);
        hash.Add(DamageType);
        hash.Add(MinPower);
        hash.Add(MaxPower);
        hash.Add(Defense);
        hash.Add(AttributeAdjustment);
        hash.Add(AttributeCombinationId);
        hash.Add(Buff);
        hash.Add(SuffixItemId);
        hash.Add(SecondarySuffixItemId);
        foreach (var attribute in Attributes)
        {
            hash.Add(attribute.Key);
            hash.Add(attribute.Value);
        }

        foreach (var slot in InfusionSlots)
        {
            hash.Add(slot);
        }

        foreach (var stat in StatChoices)
        {
            hash.Add(stat);
        }

        return hash.ToHashCode();
    }
}
