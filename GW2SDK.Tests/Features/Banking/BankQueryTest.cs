using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using GuildWars2.Banking;
using GuildWars2.Inventories;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Banking;

public class BankQueryTest
{
    // Yes the Bank is enumerable but its type has meaning
    [SuppressMessage("ReSharper", "SuggestBaseTypeForParameter")]
    private static class BankFact
    {
        public static void Bank_is_not_empty(Bank actual) => Assert.NotEmpty(actual);

        public static void Bank_tabs_have_30_slots(Bank actual) =>
            Assert.Equal(0, actual.Count % 30);
    }

    private static class MaterialCategoryFact
    {
        public static void Name_is_not_empty(MaterialCategory actual) =>
            Assert.NotEmpty(actual.Name);
    }

    private static class ItemSlotFact
    {
        public static void ItemSlot_id_is_positive(ItemSlot actual) =>
            Assert.InRange(actual.Id, 1, int.MaxValue);

        public static void ItemSlot_count_is_positive(ItemSlot actual) =>
            Assert.InRange(actual.Count, 1, int.MaxValue);
    }

    [Fact]
    public async Task Bank_contents_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Bank.GetBank(accessToken.Key);

        BankFact.Bank_is_not_empty(actual.Value);

        BankFact.Bank_tabs_have_30_slots(actual.Value);

        Assert.All(
            actual.Value,
            slot =>
            {
                if (slot is null)
                {
                    return;
                }

                ItemSlotFact.ItemSlot_id_is_positive(slot);
                ItemSlotFact.ItemSlot_count_is_positive(slot);
            }
        );
    }

    [Fact]
    public async Task Material_categories_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Bank.GetMaterialCategories();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            materialCategory =>
            {
                MaterialCategoryFact.Name_is_not_empty(materialCategory);
            }
        );
    }

    [Fact]
    public async Task Material_categories_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Bank.GetMaterialCategoriesIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_material_category_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int materialCategoryId = 5;

        var actual = await sut.Bank.GetMaterialCategoryById(materialCategoryId);

        Assert.Equal(materialCategoryId, actual.Value.Id);
    }

    [Fact]
    public async Task Material_categories_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            5,
            6,
            29
        };

        var actual = await sut.Bank.GetMaterialCategoriesByIds(ids);

        Assert.Collection(
            actual.Value,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids)
        );
    }
}
