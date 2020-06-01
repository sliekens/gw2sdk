using GW2SDK.Impl.JsonConverters;
using GW2SDK.Items;
using GW2SDK.Tests.Features.Items.Fixtures;
using GW2SDK.Tests.TestInfrastructure;
using Newtonsoft.Json;
using Xunit;
using Xunit.Abstractions;

namespace GW2SDK.Tests.Features.Items
{
    [Collection(nameof(ItemDbCollection))]
    public class ItemTest
    {
        public ItemTest(ItemFixture fixture, ITestOutputHelper output)
        {
            _fixture = fixture;
            _output = output;
        }

        private readonly ItemFixture _fixture;

        private readonly ITestOutputHelper _output;

        [Fact]
        [Trait("Feature",    "Items")]
        [Trait("Category",   "Integration")]
        [Trait("Importance", "Critical")]
        public void Items_can_be_serialized_from_json()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output))
                .UseMissingMemberHandling(MissingMemberHandling.Error)
                .Build();
            AssertEx.ForEach(_fixture.Db.Items,
                json =>
                {
                    // Next statement throws if there are missing members
                    _ = JsonConvert.DeserializeObject<Item>(json, settings);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Id_is_positive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Items,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.Id, 1, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Vendor_value_cannot_be_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Items,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.VendorValue, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Consumable_level_cannot_be_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetConsumableItems(),
                json =>
                {
                    var actual = (Consumable) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.Level, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Weapon_level_cannot_be_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetWeaponItems(),
                json =>
                {
                    var actual = (Weapon) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.Level, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Weapon_min_power_cannot_be_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetWeaponItems(),
                json =>
                {
                    var actual = (Weapon) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.MinPower, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Weapon_max_power_cannot_be_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetWeaponItems(),
                json =>
                {
                    var actual = (Weapon) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.MaxPower, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Weapon_defense_cannot_be_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetWeaponItems(),
                json =>
                {
                    var actual = (Weapon) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.Defense, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Weapon_infix_upgrade_id_is_positive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetWeaponItems(),
                json =>
                {
                    var actual = (Weapon) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (actual.InfixUpgrade is InfixUpgrade)
                    {
                        Assert.InRange(actual.InfixUpgrade.Id, 1, int.MaxValue);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Weapon_infix_upgrade_modifiers_are_positive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetWeaponItems(),
                json =>
                {
                    var Weapon = (Weapon) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (Weapon.InfixUpgrade is InfixUpgrade)
                    {
                        Assert.All(Weapon.InfixUpgrade.Attributes, actual => Assert.InRange(actual.Modifier, 1, int.MaxValue));
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Back_item_level_cannot_be_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetBackItems(),
                json =>
                {
                    var actual = (BackItem) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.Level, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Back_item_infix_upgrade_id_is_positive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetBackItems(),
                json =>
                {
                    var actual = (BackItem) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (actual.InfixUpgrade is InfixUpgrade)
                    {
                        Assert.InRange(actual.InfixUpgrade.Id, 1, int.MaxValue);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Back_item_infix_upgrade_modifiers_are_positive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetBackItems(),
                json =>
                {
                    var backItem = (BackItem) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (backItem.InfixUpgrade is InfixUpgrade)
                    {
                        Assert.All(backItem.InfixUpgrade.Attributes, actual => Assert.InRange(actual.Modifier, 1, int.MaxValue));
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Back_item_suffix_item_id_is_null_or_positive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetBackItems(),
                json =>
                {
                    var actual = (BackItem) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (actual.SuffixItemId.HasValue)
                    {
                        Assert.InRange(actual.SuffixItemId.Value, 1, int.MaxValue);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Armor_level_cannot_be_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetArmorItems(),
                json =>
                {
                    var actual = (Armor) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.Level, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Armor_defense_cannot_be_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetArmorItems(),
                json =>
                {
                    var actual = (Armor) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.Defense, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Armor_infix_upgrade_id_is_positive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetArmorItems(),
                json =>
                {
                    var actual = (Armor) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (actual.InfixUpgrade is InfixUpgrade)
                    {
                        Assert.InRange(actual.InfixUpgrade.Id, 1, int.MaxValue);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Armor_infix_upgrade_modifiers_are_positive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetArmorItems(),
                json =>
                {
                    var Armor = (Armor) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (Armor.InfixUpgrade is InfixUpgrade)
                    {
                        Assert.All(Armor.InfixUpgrade.Attributes, actual => Assert.InRange(actual.Modifier, 1, int.MaxValue));
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Armor_suffix_item_id_is_null_or_positive()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetArmorItems(),
                json =>
                {
                    var actual = (Armor) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (actual.SuffixItemId.HasValue)
                    {
                        Assert.InRange(actual.SuffixItemId.Value, 1, int.MaxValue);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Armor_stat_choices_are_null_or_not_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetArmorItems(),
                json =>
                {
                    var actual = (Armor) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (actual.StatChoices is int[])
                    {
                        Assert.NotEmpty(actual.StatChoices);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Trophy_level_cannot_be_negative()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetTrophyItems(),
                json =>
                {
                    var actual = (Trophy) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.InRange(actual.Level, 0, int.MaxValue);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Transmutation_skins_cannot_be_empty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetConsumableTransmutationItems(),
                json =>
                {
                    var actual = (Transmutation) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotEmpty(actual.Skins);
                });
        }
    }
}
