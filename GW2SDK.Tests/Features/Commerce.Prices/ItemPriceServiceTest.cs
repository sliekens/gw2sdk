using System;
using System.Threading.Tasks;
using GW2SDK.Commerce.Prices;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Commerce.Prices
{
    public class ItemPriceServiceTest
    {
        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_item_price_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemPriceService>();

            var actual = await sut.GetItemPricesIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_an_item_price_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemPriceService>();

            const int itemId = 24;

            var actual = await sut.GetItemPriceById(itemId);

            Assert.Equal(itemId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Commerce.Prices")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_item_prices_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemPriceService>();

            var ids = new[] { 24, 19699, 35984 };

            var actual = await sut.GetItemPricesByIds(ids);

            Assert.Collection(actual,
                first => Assert.Equal(24,     first.Id),
                second => Assert.Equal(19699, second.Id),
                third => Assert.Equal(35984,  third.Id));
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public async Task Item_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemPriceService>();

            await Assert.ThrowsAsync<ArgumentNullException>("itemIds",
                async () =>
                {
                    await sut.GetItemPricesByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public async Task Item_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemPriceService>();

            await Assert.ThrowsAsync<ArgumentException>("itemIds",
                async () =>
                {
                    await sut.GetItemPricesByIds(new int[0]);
                });
        }
    }
}
