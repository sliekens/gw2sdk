﻿using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GW2SDK.Accounts.Banks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Accounts.Banks
{
    public class BankServiceTest
    {
        [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")] // Yes the Bank is enumerable but its type has meaning
        private static class AccountBankFact
        {
            public static void Bank_is_not_empty(AccountBank actual) => Assert.NotEmpty(actual);

            public static void Bank_tabs_have_30_slots(AccountBank actual) => Assert.Equal(0, actual.Count % 30);
        }

        private static class MaterialCategoryFact
        {
            public static void Name_is_not_empty(MaterialCategory actual) => Assert.NotEmpty(actual.Name);
        }

        private static class AccountBankSlotFact
        {
            public static void BankSlot_id_is_positive(BankSlot actual) => Assert.InRange(actual.Id, 1, int.MaxValue);

            public static void BankSlot_count_is_positive(BankSlot actual) =>
                Assert.InRange(actual.Count, 1, int.MaxValue);

            public static void BankSlot_remembers_character_bindings(BankSlot actual)
            {
                if (string.IsNullOrEmpty(actual.BoundTo))
                {
                    Assert.NotEqual(ItemBinding.Character, actual.Binding);
                }
                else
                {
                    Assert.Equal(ItemBinding.Character, actual.Binding);
                }
            }
        }

        [Fact]
        public async Task It_can_get_the_bank()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BankService>();
            var accessToken = services.Resolve<ApiKeyFull>();

            var actual = await sut.GetBank(accessToken.Key);

            AccountBankFact.Bank_is_not_empty(actual.Value);

            AccountBankFact.Bank_tabs_have_30_slots(actual.Value);

            Assert.All(actual.Value,
                slot =>
                {
                    if (slot is null) return;
                    AccountBankSlotFact.BankSlot_id_is_positive(slot);
                    AccountBankSlotFact.BankSlot_count_is_positive(slot);
                    AccountBankSlotFact.BankSlot_remembers_character_bindings(slot);
                });
        }

        [Fact]
        public async Task It_can_get_all_material_categories()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BankService>();

            var actual = await sut.GetMaterialCategories();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
            Assert.All(actual.Values,
                materialCategory =>
                {
                    MaterialCategoryFact.Name_is_not_empty(materialCategory);
                });
        }

        [Fact]
        public async Task It_can_get_all_material_category_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BankService>();

            var actual = await sut.GetMaterialCategoriesIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        public async Task It_can_get_a_material_category_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BankService>();

            const int materialCategoryId = 5;

            var actual = await sut.GetMaterialCategoryById(materialCategoryId);

            Assert.Equal(materialCategoryId, actual.Value.Id);
        }

        [Fact]
        public async Task It_can_get_material_categories_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<BankService>();

            var ids = new[]
            {
                5,
                6,
                29
            };

            var actual = await sut.GetMaterialCategoriesByIds(ids);

            Assert.Collection(actual.Values,
                first => Assert.Equal(ids[0], first.Id),
                second => Assert.Equal(ids[1], second.Id),
                third => Assert.Equal(ids[2], third.Id));
        }
    }
}
