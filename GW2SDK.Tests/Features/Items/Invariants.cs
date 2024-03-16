using GuildWars2.Items;

namespace GuildWars2.Tests.Features.Items;

internal static class Invariants
{
    internal static void Id_is_positive(this Item actual) => Assert.True(actual.Id >= 1);

    internal static void Vendor_value_is_not_negative(this Item actual) =>
        Assert.True(actual.VendorValue.Amount >= 0);

    internal static void Level_is_between_0_and_80(this Item actual) =>
        Assert.InRange(actual.Level, 0, 80);

    internal static void Has_body_types(this Item actual) => Assert.NotEmpty(actual.BodyTypes);

    internal static void Has_races(this Item actual) => Assert.NotEmpty(actual.Races);

    internal static void Has_professions(this Item actual) => Assert.NotEmpty(actual.Professions);

    internal static void Min_power_is_not_negative(this Weapon actual) =>
        Assert.True(actual.MinPower >= 0);

    internal static void Max_power_is_not_negative(this Weapon actual) =>
        Assert.True(actual.MaxPower >= 0);

    internal static void Defense_is_not_negative(this Weapon actual) =>
        Assert.True(actual.Defense >= 0);

    internal static void Infix_upgrade_id_is_positive(this Weapon actual)
    {
        if (actual.AttributeCombinationId is not null)
        {
            Assert.True(actual.AttributeCombinationId >= 1);
        }
    }

    internal static void Infix_upgrade_modifiers_are_positive(this Weapon actual) =>
        Assert.All(actual.Attributes, attribute => Assert.True(attribute.Value >= 1));

    internal static void Prefix_and_stat_choices_are_mutually_exclusive(this Weapon actual)
    {
        if (actual.AttributeCombinationId.HasValue)
        {
            Assert.Empty(actual.StatChoices);
        }
        else if (actual.StatChoices is not null)
        {
            Assert.Null(actual.AttributeCombinationId);
            Assert.Empty(actual.Attributes);
        }
    }

    internal static void Infix_upgrade_id_is_positive(this Backpack actual)
    {
        if (actual.AttributeCombinationId.HasValue)
        {
            Assert.True(actual.AttributeCombinationId >= 1);
        }
    }

    internal static void Infix_upgrade_modifiers_are_positive(this Backpack actual) =>
        Assert.All(actual.Attributes, attribute => Assert.True(attribute.Value >= 1));

    internal static void Suffix_item_id_is_null_or_positive(this Backpack actual)
    {
        if (actual.SuffixItemId.HasValue)
        {
            Assert.True(actual.SuffixItemId.Value >= 1);
        }
    }

    internal static void Prefix_and_stat_choices_are_mutually_exclusive(this Backpack actual)
    {
        if (actual.AttributeCombinationId.HasValue)
        {
            Assert.Empty(actual.StatChoices);
        }
        else if (actual.StatChoices is not null)
        {
            Assert.Null(actual.AttributeCombinationId);
            Assert.Empty(actual.Attributes);
        }
    }

    internal static void Defense_is_not_negative(this Armor actual) =>
        Assert.True(actual.Defense >= 0);

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
        if (actual.AttributeCombinationId.HasValue)
        {
            Assert.True(actual.AttributeCombinationId >= 1);
        }
    }

    internal static void Infix_upgrade_modifiers_are_positive(this Armor actual) =>
        Assert.All(actual.Attributes, attribute => Assert.True(attribute.Value >= 1));

    internal static void Suffix_item_id_is_null_or_positive(this Armor actual)
    {
        if (actual.SuffixItemId.HasValue)
        {
            Assert.True(actual.SuffixItemId.Value >= 1);
        }
    }

    internal static void Prefix_and_stat_choices_are_mutually_exclusive(this Armor actual)
    {
        if (actual.AttributeCombinationId.HasValue)
        {
            Assert.Empty(actual.StatChoices);
        }
        else if (actual.StatChoices.Count > 0)
        {
            Assert.Null(actual.AttributeCombinationId);
            Assert.Empty(actual.Attributes);
        }
    }

    internal static void Prefix_and_stat_choices_are_mutually_exclusive(this Trinket actual)
    {
        if (actual.AttributeCombinationId.HasValue)
        {
            Assert.Empty(actual.StatChoices);
        }
        else if (actual.StatChoices is not null)
        {
            Assert.Null(actual.AttributeCombinationId);
            Assert.Empty(actual.Attributes);
        }
    }

    internal static void Skins_are_not_empty(this Transmutation actual) =>
        Assert.NotEmpty(actual.SkinIds);

    internal static void Has_charges(this SalvageTool actual) =>
        Assert.InRange(actual.Charges, 1, 250);

    internal static void MiniatureId_is_positive(this Miniature actual) =>
        Assert.True(actual.MiniatureId >= 1);

    internal static void Validate(this Item actual)
    {
        var chatLink = actual.GetChatLink();
        Assert.Equal(actual.ChatLink, chatLink.ToString());

        actual.Id_is_positive();
        actual.Vendor_value_is_not_negative();
        actual.Level_is_between_0_and_80();
        actual.Has_body_types();
        actual.Has_races();
        actual.Has_professions();
        switch (actual)
        {
            case Consumable consumable:
                switch (consumable)
                {
                    case Transmutation transmutation:
                        transmutation.Skins_are_not_empty();
                        break;
                    case RecipeSheet recipe:
                        Assert.True(recipe.Id > 0);
                        var link = recipe.GetRecipeChatLink();
                        Assert.Equal(recipe.RecipeId, link.RecipeId);
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
                armor.Infusion_slot_flags_is_not_empty();
                armor.Prefix_and_stat_choices_are_mutually_exclusive();
                break;
            case Trinket trinket:
                trinket.Prefix_and_stat_choices_are_mutually_exclusive();
                break;
            case SalvageTool salvageTool:
                salvageTool.Has_charges();
                break;
            case Miniature miniature:
                miniature.MiniatureId_is_positive();
                break;
            case UpgradeComponent upgradeComponent:
                Assert.Empty(upgradeComponent.UpgradeComponentFlags.Other);
                Assert.Empty(upgradeComponent.InfusionUpgradeFlags.Other);

                // There is a workaround in place for PvP runes and sigils not being classified as such
                if (upgradeComponent.GameTypes.Contains(GameType.Pvp))
                {
                    if (upgradeComponent.Name.Contains("Rune"))
                    {
                        Assert.IsType<Rune>(upgradeComponent);
                    }

                    if (upgradeComponent.Name.Contains("Sigil"))
                    {
                        Assert.IsType<Sigil>(upgradeComponent);
                    }
                }
                break;
        }
    }
}
