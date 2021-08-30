using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Items;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class ItemServiceTest
    {
        [Fact]
        public async Task It_can_get_all_item_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemService>();

            var actual = await sut.GetItemsIndex();

            Assert.Equal(actual.Context.ResultTotal, actual.Values.Count);
        }

        [Fact]
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
        public async Task It_can_get_items_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemService>();

            var ids = new HashSet<int>
            {
                24,
                46,
                56
            };

            var actual = await sut.GetItemsByIds(ids)
                .ToListAsync();

            Assert.Collection(actual,
                item => Assert.Equal(24, item.Value.Id),
                item => Assert.Equal(46, item.Value.Id),
                item => Assert.Equal(56, item.Value.Id));
        }

        [Fact]
        public async Task It_can_get_items_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemService>();

            var actual = await sut.GetItemsByPage(0, 3);

            Assert.Equal(3, actual.Values.Count);
            Assert.Equal(3, actual.Context.PageSize);
        }

        [Fact(Skip =
            "This test is best used interactively, otherwise it will hit rate limits in this as well as other tests.")]
        public async Task It_can_get_all_items()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemService>();

            await foreach (var actual in sut.GetItems())
            {
                ItemFacts.Validate(actual.Value);
            }
        }
    }
}
