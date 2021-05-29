﻿using System.Threading.Tasks;
using GW2SDK.ItemStats;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.ItemStats
{
    public class ItemStatServiceTest
    {
        [Fact]
        [Trait("Feature", "ItemStats")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_item_stats()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemStatService>();

            var actual = await sut.GetItemStats();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature", "ItemStats")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_all_item_stat_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemStatService>();

            var actual = await sut.GetItemStatsIndex();

            Assert.Equal(actual.ResultTotal, actual.Count);
        }

        [Fact]
        [Trait("Feature", "ItemStats")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_an_item_stat_by_id()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemStatService>();

            const int itemStatId = 559;

            var actual = await sut.GetItemStatById(itemStatId);

            Assert.Equal(itemStatId, actual.Id);
        }

        [Fact]
        [Trait("Feature", "ItemStats")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_item_stats_by_ids()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemStatService>();

            var ids = new[]
            {
                161,
                559,
                1566
            };

            var actual = await sut.GetItemStatsByIds(ids);

            Assert.Collection(actual,
                first => Assert.Equal(ids[0], first.Id),
                second => Assert.Equal(ids[1], second.Id),
                third => Assert.Equal(ids[2], third.Id));
        }

        [Fact]
        [Trait("Feature", "ItemStats")]
        [Trait("Category", "Integration")]
        public async Task It_can_get_item_stats_by_page()
        {
            await using var services = new Composer();
            var sut = services.Resolve<ItemStatService>();

            var actual = await sut.GetItemStatsByPage(1, 3);

            Assert.Equal(3, actual.Count);
            Assert.Equal(3, actual.PageSize);
            Assert.NotNull(actual.Next);

            var next = await sut.GetItemStatsByPage(actual.Next);

            Assert.Equal(actual.Next, next.Self);
        }
    }
}
