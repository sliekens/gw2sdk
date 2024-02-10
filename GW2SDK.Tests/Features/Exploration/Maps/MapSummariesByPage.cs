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

        Assert.NotNull(context.PageContext);
        Assert.Equal(pageSize, context.PageContext.PageSize);
        Assert.Equal(pageSize, actual.Count);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(pageSize, context.ResultContext.ResultCount);
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
