﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapSummariesByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var (actual, context) = await sut.Exploration.GetMapSummariesByPage(0, pageSize);

        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(pageSize, actual.Count);
        Assert.Equal(context.ResultCount, pageSize);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
            }
        );
    }
}
