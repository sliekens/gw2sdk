using System.Text.Json;
using GW2SDK.Items;
using GW2SDK.Json;
using GW2SDK.Tests.Features.Items.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class ItemReaderTest : IClassFixture<ItemFixture>
    {
        public ItemReaderTest(ItemFixture fixture)
        {
            _fixture = fixture;
        }

        private readonly ItemFixture _fixture;

        private static class ItemFacts
        {
            public static void Id_is_positive(Item actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Vendor_value_cannot_be_negative(Item actual) => Assert.InRange(actual.VendorValue, 0, int.MaxValue);

            public static void Consumable_level_is_between_0_and_80(Consumable actual) => Assert.InRange(actual.Level, 0, 80);

            public static void Weapon_level_is_between_0_and_80(Weapon actual) => Assert.InRange(actual.Level, 0, 80);

            public static void Weapon_min_power_cannot_be_negative(Weapon actual) => Assert.InRange(actual.MinPower, 0, int.MaxValue);

            public static void Weapon_max_power_cannot_be_negative(Weapon actual) => Assert.InRange(actual.MaxPower, 0, int.MaxValue);

            public static void Weapon_defense_cannot_be_negative(Weapon actual) => Assert.InRange(actual.Defense, 0, int.MaxValue);

            public static void Weapon_infix_upgrade_id_is_positive(Weapon actual)
            {
                if (actual.Prefix is InfixUpgrade)
                {
                    Assert.InRange(actual.Prefix.ItemstatsId, 1, int.MaxValue);
                }
            }

            public static void Weapon_infix_upgrade_modifiers_are_positive(Weapon weapon)
            {
                if (weapon.Prefix is InfixUpgrade)
                {
                    Assert.All(weapon.Prefix.Attributes, actual => Assert.InRange(actual.Modifier, 1, int.MaxValue));
                }
            }

            public static void Weapon_prefix_and_stat_choices_are_mutually_exclusive(Weapon weapon)
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

            public static void Backpack_level_is_between_0_and_80(Backpack actual) => Assert.InRange(actual.Level, 0, 80);

            public static void Backpack_infix_upgrade_id_is_positive(Backpack actual)
            {
                if (actual.Prefix is InfixUpgrade)
                {
                    Assert.InRange(actual.Prefix.ItemstatsId, 1, int.MaxValue);
                }
            }

            public static void Backpack_infix_upgrade_modifiers_are_positive(Backpack backpack)
            {
                if (backpack.Prefix is InfixUpgrade)
                {
                    Assert.All(backpack.Prefix.Attributes, actual => Assert.InRange(actual.Modifier, 1, int.MaxValue));
                }
            }

            public static void Backpack_suffix_item_id_is_null_or_positive(Backpack actual)
            {
                if (actual.SuffixItemId.HasValue)
                {
                    Assert.InRange(actual.SuffixItemId.Value, 1, int.MaxValue);
                }
            }
            
            public static void Backpack_prefix_and_stat_choices_are_mutually_exclusive(Backpack backpack)
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

            public static void Armor_level_is_between_0_and_80(Armor actual) => Assert.InRange(actual.Level, 0, 80);

            public static void Armor_defense_cannot_be_negative(Armor actual) => Assert.InRange(actual.Defense, 0, 1000);

            public static void Armor_infusion_slot_flags_cannot_be_empty(Armor actual)
            {
                foreach (var slot in actual.InfusionSlots)
                {
                    Assert.NotEmpty(slot.Flags);
                }
            }

            public static void Armor_infix_upgrade_id_is_positive(Armor actual)
            {
                if (actual.Prefix is InfixUpgrade)
                {
                    Assert.InRange(actual.Prefix.ItemstatsId, 1, int.MaxValue);
                }
            }

            public static void Armor_infix_upgrade_modifiers_are_positive(Armor armor)
            {
                if (armor.Prefix is InfixUpgrade)
                {
                    Assert.All(armor.Prefix.Attributes, actual => Assert.InRange(actual.Modifier, 1, int.MaxValue));
                }
            }

            public static void Armor_suffix_item_id_is_null_or_positive(Armor actual)
            {
                if (actual.SuffixItemId.HasValue)
                {
                    Assert.InRange(actual.SuffixItemId.Value, 1, int.MaxValue);
                }
            }

            public static void Armor_stat_choices_are_null_or_not_empty(Armor actual)
            {
                if (actual.StatChoices is int[])
                {
                    Assert.NotEmpty(actual.StatChoices);
                }
            }
            
            public static void Armor_prefix_and_stat_choices_are_mutually_exclusive(Armor armor)
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

            public static void Trinket_prefix_and_stat_choices_are_mutually_exclusive(Trinket trinket)
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

            public static void Trophy_level_is_between_0_and_80(Trophy actual) => Assert.InRange(actual.Level, 0, 80);

            public static void Transmutation_skins_cannot_be_empty(Transmutation actual) => Assert.NotEmpty(actual.Skins);

            public static void SalvageTool_has_charges(SalvageTool salvageTool) => Assert.InRange(salvageTool.Charges, 1, 255);

            public static void MinipetId_is_positive(Minipet minipet) => Assert.InRange(minipet.MinipetId, 1, int.MaxValue);
        }

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Items_can_be_created_from_json()
        {
            var sut = new ItemReader();

            AssertEx.ForEach(_fixture.Db.Items,
                json =>
                {
                    using var document = JsonDocument.Parse(json);
                    var actual = sut.Read(document.RootElement, MissingMemberBehavior.Error);

                    ItemFacts.Id_is_positive(actual);
                    ItemFacts.Vendor_value_cannot_be_negative(actual);
                    switch (actual)
                    {
                        case Consumable consumable:
                            ItemFacts.Consumable_level_is_between_0_and_80(consumable);
                            switch (consumable)
                            {
                                case Transmutation transmutation:
                                    ItemFacts.Transmutation_skins_cannot_be_empty(transmutation);
                                    break;
                            }

                            break;
                        case Weapon weapon:
                            ItemFacts.Weapon_level_is_between_0_and_80(weapon);
                            ItemFacts.Weapon_min_power_cannot_be_negative(weapon);
                            ItemFacts.Weapon_max_power_cannot_be_negative(weapon);
                            ItemFacts.Weapon_defense_cannot_be_negative(weapon);
                            ItemFacts.Weapon_infix_upgrade_id_is_positive(weapon);
                            ItemFacts.Weapon_infix_upgrade_modifiers_are_positive(weapon);
                            ItemFacts.Weapon_prefix_and_stat_choices_are_mutually_exclusive(weapon);
                            break;
                        case Backpack backItem:
                            ItemFacts.Backpack_level_is_between_0_and_80(backItem);
                            ItemFacts.Backpack_infix_upgrade_id_is_positive(backItem);
                            ItemFacts.Backpack_infix_upgrade_modifiers_are_positive(backItem);
                            ItemFacts.Backpack_suffix_item_id_is_null_or_positive(backItem);
                            ItemFacts.Backpack_prefix_and_stat_choices_are_mutually_exclusive(backItem);
                            break;
                        case Armor armor:
                            ItemFacts.Armor_level_is_between_0_and_80(armor);
                            ItemFacts.Armor_defense_cannot_be_negative(armor);
                            ItemFacts.Armor_infix_upgrade_id_is_positive(armor);
                            ItemFacts.Armor_infix_upgrade_modifiers_are_positive(armor);
                            ItemFacts.Armor_suffix_item_id_is_null_or_positive(armor);
                            ItemFacts.Armor_stat_choices_are_null_or_not_empty(armor);
                            ItemFacts.Armor_infusion_slot_flags_cannot_be_empty(armor);
                            ItemFacts.Armor_prefix_and_stat_choices_are_mutually_exclusive(armor);
                            break;
                        case Trinket trinket:
                            ItemFacts.Trinket_prefix_and_stat_choices_are_mutually_exclusive(trinket);
                            break;
                        case Trophy trophy:
                            ItemFacts.Trophy_level_is_between_0_and_80(trophy);
                            break;
                        case SalvageTool salvage:
                            ItemFacts.SalvageTool_has_charges(salvage);
                            break;
                        case Minipet minipet:
                            ItemFacts.MinipetId_is_positive(minipet);
                            break;
                    }
                });
        }
    }
}
