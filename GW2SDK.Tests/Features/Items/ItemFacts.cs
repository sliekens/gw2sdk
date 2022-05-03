using System.Diagnostics.CodeAnalysis;
using GW2SDK.Items;
using Xunit;

namespace GW2SDK.Tests.Features.Items;

[SuppressMessage("ReSharper", "MemberCanBePrivate.Global")]
internal static class ItemFacts
{
    internal static void Id_is_positive(Item actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Vendor_value_is_not_negative(Item actual) =>
        Assert.InRange(actual.VendorValue.Amount, 0, int.MaxValue);

    internal static void Level_is_between_0_and_80(Item actual) =>
        Assert.InRange(actual.Level, 0, 80);

    internal static void Weapon_min_power_is_not_negative(Weapon actual) =>
        Assert.InRange(actual.MinPower, 0, int.MaxValue);

    internal static void Weapon_max_power_is_not_negative(Weapon actual) =>
        Assert.InRange(actual.MaxPower, 0, int.MaxValue);

    internal static void Weapon_defense_is_not_negative(Weapon actual) =>
        Assert.InRange(actual.Defense, 0, int.MaxValue);

    internal static void Weapon_infix_upgrade_id_is_positive(Weapon actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.InRange(actual.Prefix.ItemstatsId, 1, int.MaxValue);
        }
    }

    internal static void Weapon_infix_upgrade_modifiers_are_positive(Weapon weapon)
    {
        if (weapon.Prefix is not null)
        {
            Assert.All(
                weapon.Prefix.Attributes,
                actual => Assert.InRange(actual.Modifier, 1, int.MaxValue)
            );
        }
    }

    internal static void Weapon_prefix_and_stat_choices_are_mutually_exclusive(Weapon weapon)
    {
        if (weapon.Prefix is not null)
        {
            Assert.Null(weapon.StatChoices);
        }
        else if (weapon.StatChoices is not null)
        {
            Assert.Null(weapon.Prefix);
        }
    }

    internal static void Backpack_infix_upgrade_id_is_positive(Backpack actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.InRange(actual.Prefix.ItemstatsId, 1, int.MaxValue);
        }
    }

    internal static void Backpack_infix_upgrade_modifiers_are_positive(Backpack backpack)
    {
        if (backpack.Prefix is not null)
        {
            Assert.All(
                backpack.Prefix.Attributes,
                actual => Assert.InRange(actual.Modifier, 1, int.MaxValue)
            );
        }
    }

    internal static void Backpack_suffix_item_id_is_null_or_positive(Backpack actual)
    {
        if (actual.SuffixItemId.HasValue)
        {
            Assert.InRange(actual.SuffixItemId.Value, 1, int.MaxValue);
        }
    }

    internal static void Backpack_prefix_and_stat_choices_are_mutually_exclusive(Backpack backpack)
    {
        if (backpack.Prefix is not null)
        {
            Assert.Null(backpack.StatChoices);
        }
        else if (backpack.StatChoices is not null)
        {
            Assert.Null(backpack.Prefix);
        }
    }

    internal static void Armor_defense_is_not_negative(Armor actual) =>
        Assert.InRange(actual.Defense, 0, 1000);

    internal static void Armor_infusion_slot_flags_is_not_empty(Armor actual)
    {
        foreach (var slot in actual.InfusionSlots)
        {
            Assert.NotEmpty(slot.Flags);
        }
    }

    internal static void Armor_infix_upgrade_id_is_positive(Armor actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.InRange(actual.Prefix.ItemstatsId, 1, int.MaxValue);
        }
    }

    internal static void Armor_infix_upgrade_modifiers_are_positive(Armor armor)
    {
        if (armor.Prefix is not null)
        {
            Assert.All(
                armor.Prefix.Attributes,
                actual => Assert.InRange(actual.Modifier, 1, int.MaxValue)
            );
        }
    }

    internal static void Armor_suffix_item_id_is_null_or_positive(Armor actual)
    {
        if (actual.SuffixItemId.HasValue)
        {
            Assert.InRange(actual.SuffixItemId.Value, 1, int.MaxValue);
        }
    }

    internal static void Armor_stat_choices_are_null_or_not_empty(Armor actual)
    {
        if (actual.StatChoices is int[])
        {
            Assert.NotEmpty(actual.StatChoices);
        }
    }

    internal static void Armor_prefix_and_stat_choices_are_mutually_exclusive(Armor armor)
    {
        if (armor.Prefix is not null)
        {
            Assert.Null(armor.StatChoices);
        }
        else if (armor.StatChoices is not null)
        {
            Assert.Null(armor.Prefix);
        }
    }

    internal static void Trinket_prefix_and_stat_choices_are_mutually_exclusive(Trinket trinket)
    {
        if (trinket.Prefix is not null)
        {
            Assert.Null(trinket.StatChoices);
        }
        else if (trinket.StatChoices is not null)
        {
            Assert.Null(trinket.Prefix);
        }
    }

    internal static void Transmutation_skins_is_not_empty(Transmutation actual) =>
        Assert.NotEmpty(actual.Skins);

    internal static void SalvageTool_has_charges(SalvageTool salvageTool) =>
        Assert.InRange(salvageTool.Charges, 1, 255);

    internal static void MinipetId_is_positive(Minipet minipet) =>
        Assert.InRange(minipet.MinipetId, 1, int.MaxValue);

    internal static void Validate(Item actual)
    {
        Id_is_positive(actual);
        Vendor_value_is_not_negative(actual);
        Level_is_between_0_and_80(actual);
        switch (actual)
        {
            case Consumable consumable:
                switch (consumable)
                {
                    case Transmutation transmutation:
                        Transmutation_skins_is_not_empty(transmutation);
                        break;
                }

                break;
            case Weapon weapon:
                Weapon_min_power_is_not_negative(weapon);
                Weapon_max_power_is_not_negative(weapon);
                Weapon_defense_is_not_negative(weapon);
                Weapon_infix_upgrade_id_is_positive(weapon);
                Weapon_infix_upgrade_modifiers_are_positive(weapon);
                Weapon_prefix_and_stat_choices_are_mutually_exclusive(weapon);
                break;
            case Backpack backItem:
                Backpack_infix_upgrade_id_is_positive(backItem);
                Backpack_infix_upgrade_modifiers_are_positive(backItem);
                Backpack_suffix_item_id_is_null_or_positive(backItem);
                Backpack_prefix_and_stat_choices_are_mutually_exclusive(backItem);
                break;
            case Armor armor:
                Armor_defense_is_not_negative(armor);
                Armor_infix_upgrade_id_is_positive(armor);
                Armor_infix_upgrade_modifiers_are_positive(armor);
                Armor_suffix_item_id_is_null_or_positive(armor);
                Armor_stat_choices_are_null_or_not_empty(armor);
                Armor_infusion_slot_flags_is_not_empty(armor);
                Armor_prefix_and_stat_choices_are_mutually_exclusive(armor);
                break;
            case Trinket trinket:
                Trinket_prefix_and_stat_choices_are_mutually_exclusive(trinket);
                break;
            case SalvageTool salvage:
                SalvageTool_has_charges(salvage);
                break;
            case Minipet minipet:
                MinipetId_is_positive(minipet);
                break;
        }
    }
}
