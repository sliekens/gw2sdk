using System.Text.Json;
using GW2SDK.Impl.JsonReaders;
using GW2SDK.Items;
using GW2SDK.Items.Impl;
using GW2SDK.Tests.Features.Items.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Items
{
    //public class Impl
    //{
    //    public Item Read(JsonElement element)
    //    {
    //        int id = default;
    //        string name = default;

    //        var discriminator = GetDiscriminator(element);
    //        if (discriminator == "Armor")
    //        {
    //            int level = default;

    //            foreach (var prop in element.EnumerateObject())
    //            {
    //                if (prop.NameEquals("id"))
    //                {
    //                    id = prop.Value.GetInt32();
    //                }
    //                else if (prop.NameEquals("name"))
    //                {
    //                    name = prop.Value.GetString();
    //                }
    //                else if (prop.NameEquals("level"))
    //                {
    //                    level = prop.Value.GetInt32();
    //                }
    //            }

    //            return new Armor
    //            {
    //                Id = id,
    //                Name = name,
    //                Level = level
    //            };
    //        }

    //        if (discriminator == "Weapon")
    //        {
    //            int level = default;

    //            foreach (var prop in element.EnumerateObject())
    //            {
    //                if (prop.NameEquals("id"))
    //                {
    //                    id = prop.Value.GetInt32();
    //                }
    //                else if (prop.NameEquals("name"))
    //                {
    //                    name = prop.Value.GetString();
    //                }
    //                else if (prop.NameEquals("level"))
    //                {
    //                    level = prop.Value.GetInt32();
    //                }
    //            }

    //            return new Weapon
    //            {
    //                Id = id,
    //                Name = name,
    //                Level = level
    //            };
    //        }

    //        foreach (var prop in element.EnumerateObject())
    //        {
    //            if (prop.NameEquals("id"))
    //            {
    //                id = prop.Value.GetInt32();
    //            }
    //            else if (prop.NameEquals("name"))
    //            {
    //                name = prop.Value.GetString();
    //            }
    //        }

    //        return new Item
    //        {
    //            Id = id,
    //            Name = name
    //        };
    //    }

    //    private string GetDiscriminator
    //        (JsonElement element) =>
    //        element.GetProperty("type").GetString();
    //}

    [Collection(nameof(ItemDbCollection))]
    public class ItemJsonReaderTest
    {
        public ItemJsonReaderTest(ItemFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ItemFixture _fixture;

        private readonly ITestOutputHelper _output;

        private static class ItemFacts
        {
            public static void Id_is_positive
                (Item actual) =>
                Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void Vendor_value_cannot_be_negative
                (Item actual) =>
                Assert.InRange(actual.VendorValue, 0, int.MaxValue);

            public static void Consumable_level_is_between_0_and_80
                (Consumable actual) =>
                Assert.InRange(actual.Level, 0, 80);

            public static void Weapon_level_is_between_0_and_80
                (Weapon actual) =>
                Assert.InRange(actual.Level, 0, 80);

            public static void Weapon_min_power_cannot_be_negative
                (Weapon actual) =>
                Assert.InRange(actual.MinPower, 0, int.MaxValue);

            public static void Weapon_max_power_cannot_be_negative
                (Weapon actual) =>
                Assert.InRange(actual.MaxPower, 0, int.MaxValue);

            public static void Weapon_defense_cannot_be_negative
                (Weapon actual) =>
                Assert.InRange(actual.Defense, 0, int.MaxValue);

            public static void Weapon_infix_upgrade_id_is_positive(Weapon actual)
            {
                if (actual.InfixUpgrade is InfixUpgrade)
                {
                    Assert.InRange(actual.InfixUpgrade.Id, 1, int.MaxValue);
                }
            }

            public static void Weapon_infix_upgrade_modifiers_are_positive(Weapon weapon)
            {
                if (weapon.InfixUpgrade is InfixUpgrade)
                {
                    Assert.All
                    (
                        weapon.InfixUpgrade.Attributes,
                        actual => Assert.InRange(actual.Modifier, 1, int.MaxValue)
                    );
                }
            }

            public static void Back_item_level_is_between_0_and_80
                (BackItem actual) =>
                Assert.InRange(actual.Level, 0, 80);

            public static void Back_item_infix_upgrade_id_is_positive(BackItem actual)
            {
                if (actual.InfixUpgrade is InfixUpgrade)
                {
                    Assert.InRange(actual.InfixUpgrade.Id, 1, int.MaxValue);
                }
            }

            public static void Back_item_infix_upgrade_modifiers_are_positive(BackItem backItem)
            {
                if (backItem.InfixUpgrade is InfixUpgrade)
                {
                    Assert.All
                    (
                        backItem.InfixUpgrade.Attributes,
                        actual => Assert.InRange(actual.Modifier, 1, int.MaxValue)
                    );
                }
            }

            public static void Back_item_suffix_item_id_is_null_or_positive(BackItem actual)
            {
                if (actual.SuffixItemId.HasValue)
                {
                    Assert.InRange(actual.SuffixItemId.Value, 1, int.MaxValue);
                }
            }

            public static void Armor_level_is_between_0_and_80
                (Armor actual) =>
                Assert.InRange(actual.Level, 0, 80);

            public static void Armor_defense_cannot_be_negative
                (Armor actual) =>
                Assert.InRange(actual.Defense, 0, 1000);

            public static void Armor_infix_upgrade_id_is_positive(Armor actual)
            {
                if (actual.InfixUpgrade is InfixUpgrade)
                {
                    Assert.InRange(actual.InfixUpgrade.Id, 1, int.MaxValue);
                }
            }

            public static void Armor_infix_upgrade_modifiers_are_positive(Armor armor)
            {
                if (armor.InfixUpgrade is InfixUpgrade)
                {
                    Assert.All
                    (
                        armor.InfixUpgrade.Attributes,
                        actual => Assert.InRange(actual.Modifier, 1, int.MaxValue)
                    );
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

            public static void Trophy_level_is_between_0_and_80
                (Trophy actual) =>
                Assert.InRange(actual.Level, 0, 80);

            public static void Transmutation_skins_cannot_be_empty
                (Transmutation actual) =>
                Assert.NotEmpty(actual.Skins);
        }

        [Fact]
        [Trait("Feature", "Items")]
        [Trait("Category", "Integration")]
        [Trait("Importance", "Critical")]
        public void Items_can_be_created_from_json()
        {
            IJsonReader<Item> sut = new ItemJsonReader();

            AssertEx.ForEach
            (
                _fixture.Db.Items,
                json =>
                {
                    if (!json.Contains("\"type\": \"Armor\"")) return;
                    using var document = JsonDocument.Parse(json);
                    var actual = sut.Read(document);
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
                            break;
                        case BackItem backItem:
                            ItemFacts.Back_item_level_is_between_0_and_80(backItem);
                            ItemFacts.Back_item_infix_upgrade_id_is_positive(backItem);
                            ItemFacts.Back_item_infix_upgrade_modifiers_are_positive(backItem);
                            ItemFacts.Back_item_suffix_item_id_is_null_or_positive(backItem);
                            break;
                        case Armor armor:
                            ItemFacts.Armor_level_is_between_0_and_80(armor);
                            ItemFacts.Armor_defense_cannot_be_negative(armor);
                            ItemFacts.Armor_infix_upgrade_id_is_positive(armor);
                            ItemFacts.Armor_infix_upgrade_modifiers_are_positive(armor);
                            ItemFacts.Armor_suffix_item_id_is_null_or_positive(armor);
                            ItemFacts.Armor_stat_choices_are_null_or_not_empty(armor);
                            break;
                        case Trophy trophy:
                            ItemFacts.Trophy_level_is_between_0_and_80(trophy);
                            break;
                    }
                }
            );
        }
    }
}
