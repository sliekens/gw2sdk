﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapSummaries
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetMapSummaries();

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
            }
        );
    }
}