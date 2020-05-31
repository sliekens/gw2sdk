﻿using System;
using System.Threading.Tasks;
using GW2SDK.Items;
using Xunit;
using Container = GW2SDK.Tests.TestInfrastructure.Container;

namespace GW2SDK.Tests.Features.Items
{
    public class ItemServiceTest
    {
        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task Get_all_item_ids()
        {
            await using var services = new Container();
            var sut = services.Resolve<ItemService>();

            var actual = await sut.GetItemsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task Get_an_item_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<ItemService>();

            const int itemId = 24;

            var actual = await sut.GetItemById(itemId);

            Assert.Equal(itemId, actual.Id);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task Get_items_by_id()
        {
            await using var services = new Container();
            var sut = services.Resolve<ItemService>();

            var ids = new[] { 24, 46, 56 };

            var actual = await sut.GetItemsByIds(ids);

            Assert.Collection(actual, item => Assert.Equal(24, item.Id), item => Assert.Equal(46, item.Id), item => Assert.Equal(56, item.Id));
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Unit")]
        public async Task Item_ids_cannot_be_null()
        {
            await using var services = new Container();
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
            await using var services = new Container();
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
        public async Task Get_items_by_page()
        {
            await using var services = new Container();
            var sut = services.Resolve<ItemService>();

            var actual = await sut.GetItemsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task Page_index_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<ItemService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetItemsByPage(-1, 3));
        }

        [Fact]
        [Trait("Feature",  "Items")]
        [Trait("Category", "Integration")]
        public async Task Page_size_cannot_be_negative()
        {
            await using var services = new Container();
            var sut = services.Resolve<ItemService>();

            await Assert.ThrowsAsync<ArgumentException>(async () => await sut.GetItemsByPage(1, -3));
        }
    }
}
