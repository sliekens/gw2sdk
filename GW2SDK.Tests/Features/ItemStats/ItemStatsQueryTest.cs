using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.ItemStats;

public class ItemStatsQueryTest
{
    [Fact]
    public async Task Item_stats_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.ItemStats.GetItemStats();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task Item_stats_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.ItemStats.GetItemStatsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task An_item_stat_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int itemStatId = 559;

        var actual = await sut.ItemStats.GetItemStatById(itemStatId);

        Assert.Equal(itemStatId, actual.Value.Id);
    }

    [Fact]
    public async Task Item_stats_can_be_filtered_by_ids()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            161,
            559,
            1566
        };

        var actual = await sut.ItemStats.GetItemStatsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids)
            );
    }

    [Fact]
    public async Task Item_stats_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.ItemStats.GetItemStatsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
        Assert.False(actual.Context.Next.IsEmpty);
    }
}
