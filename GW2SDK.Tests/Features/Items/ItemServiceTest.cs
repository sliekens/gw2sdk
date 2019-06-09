using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GW2SDK.Features.Items;
using GW2SDK.Tests.Shared;
using Xunit;

namespace GW2SDK.Tests.Features.Items
{
    public class ItemServiceTest
    {
        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task GetItemIndex_ShouldReturnAllItemIds()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ItemService(http);

            var actual = await sut.GetItemIndex();

            Assert.NotEmpty(actual);
            Assert.Equal(actual.ResultCount, actual.Count);
            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task GetItemById_ShouldReturnRequestedItem()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ItemService(http);

            const int itemId = 24;

            var actual = await sut.GetItemById(itemId);

            Assert.Equal(itemId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task GetItemsByIds_ShouldReturnRequestedItems()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ItemService(http);

            var actual = await sut.GetItemsByIds(new List<int> { 24, 46, 56 });

            Assert.NotEmpty(actual);
            Assert.Collection(actual, item => Assert.Equal(24, item.Id), item => Assert.Equal(46, item.Id), item => Assert.Equal(56, item.Id));
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public async Task GetItemsByIds_WithIdsNull_ShouldThrowArgumentNullException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ItemService(http);

            await Assert.ThrowsAsync<ArgumentNullException>("itemIds",
                async () =>
                {
                    await sut.GetItemsByIds(null);
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public async Task GetItemsByIds_WithIdsEmpty_ShouldThrowArgumentException()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ItemService(http);

            await Assert.ThrowsAsync<ArgumentException>("itemIds",
                async () =>
                {
                    await sut.GetItemsByIds(Enumerable.Empty<int>().ToList());
                });
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task GetItemsByPage_WithPageSize200_ShouldReturn200Items()
        {
            var http = HttpClientFactory.CreateDefault();

            var sut = new ItemService(http);

            var actual = await sut.GetItemsByPage(0, 200);

            Assert.NotEmpty(actual);
            Assert.Equal(actual.Count, actual.ResultCount);
            Assert.Equal(200,          actual.PageSize);
        }
    }
}
