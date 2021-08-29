using System;
using System.Threading.Tasks;
using GW2SDK.Items;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class ItemServiceTest
    {
        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_item_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemService>();

            var actual = await sut.GetItemsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_an_item_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemService>();

            const int itemId = 24;

            var actual = await sut.GetItemById(itemId);

            if (actual.HasValue)
            {
                Assert.Equal(itemId, actual.Value.Id);
            }
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_items_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemService>();

            var ids = new[] { 24, 46, 56 };

            var actual = await sut.GetItemsByIds(ids);

            Assert.Collection(actual.Values, item => Assert.Equal(24, item.Id), item => Assert.Equal(46, item.Id), item => Assert.Equal(56, item.Id));
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public async Task Item_ids_cannot_be_null()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemService>();

            await Assert.ThrowsAsync<ArgumentNullException>("itemIds",
                async () =>
                {
                    await sut.GetItemsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public async Task Item_ids_cannot_be_empty()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemService>();

            await Assert.ThrowsAsync<ArgumentException>("itemIds",
                async () =>
                {
                    await sut.GetItemsByIds(new int[0]);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_items_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemService>();

            var actual = await sut.GetItemsByPage(0, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }
    }
}
