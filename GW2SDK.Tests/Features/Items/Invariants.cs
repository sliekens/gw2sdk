using GuildWars2.Items;

namespace GuildWars2.Tests.Features.Items;

internal static class Invariants
{
    internal static void Id_is_positive(this Item actual) =>
        Assert.InRange(actual.Id, 1, int.MaxValue);

    internal static void Vendor_value_is_not_negative(this Item actual) =>
        Assert.InRange(actual.VendorValue.Amount, 0, int.MaxValue);

    internal static void Level_is_between_0_and_80(this Item actual) =>
        Assert.InRange(actual.Level, 0, 80);

    internal static void Min_power_is_not_negative(this Weapon actual) =>
        Assert.InRange(actual.MinPower, 0, int.MaxValue);

    internal static void Max_power_is_not_negative(this Weapon actual) =>
        Assert.InRange(actual.MaxPower, 0, int.MaxValue);

    internal static void Defense_is_not_negative(this Weapon actual) =>
        Assert.InRange(actual.Defense, 0, int.MaxValue);

    internal static void Infix_upgrade_id_is_positive(this Weapon actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.InRange(actual.Prefix.ItemstatsId, 1, int.MaxValue);
        }
    }

    internal static void Infix_upgrade_modifiers_are_positive(this Weapon actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.All(
                actual.Prefix.Attributes,
                attribute => Assert.InRange(attribute.Modifier, 1, int.MaxValue)
            );
        }
    }

    internal static void Prefix_and_stat_choices_are_mutually_exclusive(this Weapon actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.Null(actual.StatChoices);
        }
        else if (actual.StatChoices is not null)
        {
            Assert.Null(actual.Prefix);
        }
    }

    internal static void Infix_upgrade_id_is_positive(this Backpack actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.InRange(actual.Prefix.ItemstatsId, 1, int.MaxValue);
        }
    }

    internal static void Infix_upgrade_modifiers_are_positive(this Backpack actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.All(
                actual.Prefix.Attributes,
                actual => Assert.InRange(actual.Modifier, 1, int.MaxValue)
            );
        }
    }

    internal static void Suffix_item_id_is_null_or_positive(this Backpack actual)
    {
        if (actual.SuffixItemId.HasValue)
        {
            Assert.InRange(actual.SuffixItemId.Value, 1, int.MaxValue);
        }
    }

    internal static void Prefix_and_stat_choices_are_mutually_exclusive(this Backpack actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.Null(actual.StatChoices);
        }
        else if (actual.StatChoices is not null)
        {
            Assert.Null(actual.Prefix);
        }
    }

    internal static void Defense_is_not_negative(this Armor actual) =>
        Assert.InRange(actual.Defense, 0, 1000);

    internal static void Infusion_slot_flags_is_not_empty(this Armor actual)
    {
        foreach (var slot in actual.InfusionSlots)
        {
            Assert.NotNull(slot.Flags);
            Assert.True(slot.Flags.Enrichment || slot.Flags.Infusion);
            Assert.Empty(slot.Flags.Other);
        }
    }

    internal static void Infix_upgrade_id_is_positive(this Armor actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.InRange(actual.Prefix.ItemstatsId, 1, int.MaxValue);
        }
    }

    internal static void Infix_upgrade_modifiers_are_positive(this Armor actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.All(
                actual.Prefix.Attributes,
                attribute => Assert.InRange(attribute.Modifier, 1, int.MaxValue)
            );
        }
    }

    internal static void Suffix_item_id_is_null_or_positive(this Armor actual)
    {
        if (actual.SuffixItemId.HasValue)
        {
            Assert.InRange(actual.SuffixItemId.Value, 1, int.MaxValue);
        }
    }

    internal static void Stat_choices_are_null_or_not_empty(this Armor actual)
    {
        if (actual.StatChoices is int[])
        {
            Assert.NotEmpty(actual.StatChoices);
        }
    }

    internal static void Prefix_and_stat_choices_are_mutually_exclusive(this Armor actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.Null(actual.StatChoices);
        }
        else if (actual.StatChoices is not null)
        {
            Assert.Null(actual.Prefix);
        }
    }

    internal static void Prefix_and_stat_choices_are_mutually_exclusive(this Trinket actual)
    {
        if (actual.Prefix is not null)
        {
            Assert.Null(actual.StatChoices);
        }
        else if (actual.StatChoices is not null)
        {
            Assert.Null(actual.Prefix);
        }
    }

    internal static void Skins_are_not_empty(this Transmutation actual) =>
        Assert.NotEmpty(actual.Skins);

    internal static void Has_charges(this SalvageTool actual) =>
        Assert.InRange(actual.Charges, 1, 255);

    internal static void MinipetId_is_positive(this Minipet actual) =>
        Assert.InRange(actual.MinipetId, 1, int.MaxValue);

    internal static void Validate(this Item actual)
    {
        actual.Id_is_positive();
        actual.Vendor_value_is_not_negative();
        actual.Level_is_between_0_and_80();
        switch (actual)
        {
            case Consumable consumable:
                switch (consumable)
                {
                    case Transmutation transmutation:
                        transmutation.Skins_are_not_empty();
                        break;
                }

                break;
            case Weapon weapon:
                weapon.Min_power_is_not_negative();
                weapon.Max_power_is_not_negative();
                weapon.Defense_is_not_negative();
                weapon.Infix_upgrade_id_is_positive();
                weapon.Infix_upgrade_modifiers_are_positive();
                weapon.Prefix_and_stat_choices_are_mutually_exclusive();
                break;
            case Backpack backItem:
                backItem.Infix_upgrade_id_is_positive();
                backItem.Infix_upgrade_modifiers_are_positive();
                backItem.Suffix_item_id_is_null_or_positive();
                backItem.Prefix_and_stat_choices_are_mutually_exclusive();
                break;
            case Armor armor:
                armor.Defense_is_not_negative();
                armor.Infix_upgrade_id_is_positive();
                armor.Infix_upgrade_modifiers_are_positive();
                armor.Suffix_item_id_is_null_or_positive();
                armor.Stat_choices_are_null_or_not_empty();
                armor.Infusion_slot_flags_is_not_empty();
                armor.Prefix_and_stat_choices_are_mutually_exclusive();
                break;
            case Trinket trinket:
                trinket.Prefix_and_stat_choices_are_mutually_exclusive();
                break;
            case SalvageTool salvageTool:
                salvageTool.Has_charges();
                break;
            case Minipet minipet:
                minipet.MinipetId_is_positive();
                break;
        }
    }
}
