﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.ItemStats;

public class ItemStatsQueryTest
{
    [Fact]
    public async Task Item_stats_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.ItemStats.GetItemStats();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task Item_stats_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.ItemStats.GetItemStatsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task An_item_stat_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 559;

        var actual = await sut.ItemStats.GetItemStatById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Item_stats_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            161,
            559,
            1566
        };

        var actual = await sut.ItemStats.GetItemStatsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Item_stats_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.ItemStats.GetItemStatsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
        Assert.False(actual.PageContext.Next.IsEmpty);
    }
}
