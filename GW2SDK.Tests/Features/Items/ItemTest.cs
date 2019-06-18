using GW2SDK.Features.Items;
using GW2SDK.Infrastructure;
using GW2SDK.Tests.Features.Items.Fixtures;
using GW2SDK.Tests.Shared;
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
        public void Class_ShouldHaveNoMissingMembers()
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
        public void Id_ShouldBePositive()
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
        public void Name_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Items,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.Name);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void VendorValue_ShouldNotBeNegative()
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
        public void GameTypes_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Items,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.GameTypes);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Flags_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Items,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.Flags);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void Restrictions_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Items,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.Restrictions);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void ChatLink_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.Items,
                json =>
                {
                    var actual = JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.ChatLink);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void ConsumableLevel_ShouldNotBeNegative()
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
        public void WeaponLevel_ShouldNotBeNegative()
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
        public void WeaponMinPower_ShouldNotBeNegative()
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
        public void WeaponMaxPower_ShouldNotBeNegative()
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
        public void WeaponDefense_ShouldNotBeNegative()
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
        public void WeaponInfusionSlots_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetWeaponItems(),
                json =>
                {
                    var actual = (Weapon) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.InfusionSlots);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void WeaponSecondarySuffixItemId_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetWeaponItems(),
                json =>
                {
                    var actual = (Weapon) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.SecondarySuffixItemId);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void WeaponInfixUpgradeId_ShouldBePositive()
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
        public void WeaponInfixUpgradeAttributes_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetWeaponItems(),
                json =>
                {
                    var actual = (Weapon) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (actual.InfixUpgrade is InfixUpgrade)
                    {
                        Assert.NotNull(actual.InfixUpgrade.Attributes);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void WeaponInfixUpgradeModifiers_ShouldBePositive()
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
        public void BackItemLevel_ShouldNotBeNegative()
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
        public void BackItemDefaultSkin_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetBackItems(),
                json =>
                {
                    var actual = (BackItem) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.DefaultSkin);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void BackItemInfusionSlots_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetBackItems(),
                json =>
                {
                    var actual = (BackItem) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.InfusionSlots);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void BackItemInfixUpgradeId_ShouldBePositive()
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
        public void BackItemInfixUpgradeAttributes_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetBackItems(),
                json =>
                {
                    var actual = (BackItem) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (actual.InfixUpgrade is InfixUpgrade)
                    {
                        Assert.NotNull(actual.InfixUpgrade.Attributes);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void BackItemInfixUpgradeModifiers_ShouldBePositive()
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
        public void BackItemSecondarySuffixItemId_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetBackItems(),
                json =>
                {
                    var actual = (BackItem) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.SecondarySuffixItemId);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void BackItemSuffixItemId_WhenSuffixItemIdHasValue_ShouldBePositive()
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
        public void ArmorLevel_ShouldNotBeNegative()
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
        public void ArmorDefaultSkin_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetArmorItems(),
                json =>
                {
                    var actual = (Armor) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.DefaultSkin);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void ArmorDefense_ShouldNotBeNegative()
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
        public void ArmorInfusionSlots_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetArmorItems(),
                json =>
                {
                    var actual = (Armor) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.InfusionSlots);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void ArmorInfixUpgradeId_ShouldBePositive()
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
        public void ArmorInfixUpgradeAttributes_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetArmorItems(),
                json =>
                {
                    var actual = (Armor) JsonConvert.DeserializeObject<Item>(json, settings);
                    if (actual.InfixUpgrade is InfixUpgrade)
                    {
                        Assert.NotNull(actual.InfixUpgrade.Attributes);
                    }
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void ArmorInfixUpgradeModifiers_ShouldBePositive()
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
        public void ArmorSecondarySuffixItemId_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetArmorItems(),
                json =>
                {
                    var actual = (Armor) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.SecondarySuffixItemId);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void ArmorSuffixItemId_WhenSuffixItemIdHasValue_ShouldBePositive()
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
        public void ArmorStatChoices_WhenNotNull_ShouldNotBeEmpty()
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
        public void TrophyLevel_ShouldNotBeNegative()
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
        public void TransmutationSkins_ShouldNotBeEmpty()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetConsumableTransmutationItems(),
                json =>
                {
                    var actual = (Transmutation) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotEmpty(actual.Skins);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public void UpgradeComponentFlags_ShouldNotBeNull()
        {
            var settings = new JsonSerializerSettingsBuilder().UseTraceWriter(new XunitTraceWriter(_output)).Build();
            AssertEx.ForEach(_fixture.Db.GetUpgradeComponentItems(),
                json =>
                {
                    var actual = (UpgradeComponent) JsonConvert.DeserializeObject<Item>(json, settings);
                    Assert.NotNull(actual.UpgradeComponentFlags);
                });
        }
    }
}
