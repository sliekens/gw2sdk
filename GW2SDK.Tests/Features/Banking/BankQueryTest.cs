﻿using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GW2SDK.Banking;
using GW2SDK.Banking.Models;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Banking;

public class BankQueryTest
{
    // Yes the Bank is enumerable but its type has meaning
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    private static class AccountBankFact
    {
        public static void Bank_is_not_empty(AccountBank actual) => Assert.NotEmpty(actual);

        public static void Bank_tabs_have_30_slots(AccountBank actual) =>
            Assert.Equal(0, actual.Count % 30);
    }

    private static class MaterialCategoryFact
    {
        public static void Name_is_not_empty(MaterialCategory actual) =>
            Assert.NotEmpty(actual.Name);
    }

    private static class AccountBankSlotFact
    {
        public static void BankSlot_id_is_positive(BankSlot actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void BankSlot_count_is_positive(BankSlot actual) =>
            Assert.InRange(actual.Count, 1, int.MaxValue);
    }

    [Fact]
    public async Task Bank_contents_can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<BankQuery>();
        var accessToken = services.Resolve<ApiKeyFull>();

        var actual = await sut.GetBank(accessToken.Key);

        AccountBankFact.Bank_is_not_empty(actual.Value);

        AccountBankFact.Bank_tabs_have_30_slots(actual.Value);

        Assert.All(
            actual.Value,
            slot =>
            {
                if (slot is null)
                {
                    return;
                }

                AccountBankSlotFact.BankSlot_id_is_positive(slot);
                AccountBankSlotFact.BankSlot_count_is_positive(slot);
            }
            );
    }

    [Fact]
    public async Task Material_categories_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<BankQuery>();

        var actual = await sut.GetMaterialCategories();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            materialCategory =>
            {
                MaterialCategoryFact.Name_is_not_empty(materialCategory);
            }
            );
    }

    [Fact]
    public async Task Material_categories_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<BankQuery>();

        var actual = await sut.GetMaterialCategoriesIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_material_category_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<BankQuery>();

        const int materialCategoryId = 5;

        var actual = await sut.GetMaterialCategoryById(materialCategoryId);

        Assert.Equal(materialCategoryId, actual.Value.Id);
    }

    [Fact]
    public async Task Material_categories_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<BankQuery>();

        HashSet<int> ids = new()
        {
            5,
            6,
            29
        };

        var actual = await sut.GetMaterialCategoriesByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids)
            );
    }
}
