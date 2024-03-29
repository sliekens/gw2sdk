﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Continents;

public class ContinentsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int pageSize = 3;
        var (actual, context) = await sut.Exploration.GetContinentsByPage(0, pageSize);

        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(2, actual.Count);
        Assert.Equal(context.ResultCount, 2);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_dimensions();
                entry.Has_min_zoom();
                entry.Has_max_zoom();
                entry.Has_floors();
            }
        );
    }
}
