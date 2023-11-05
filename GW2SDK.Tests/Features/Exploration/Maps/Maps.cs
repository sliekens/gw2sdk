﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class Maps
{
    [Theory]
    [InlineData(1, 0, 1)]
    [InlineData(1, 0, 2)]
    [InlineData(1, 0, 3)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Maps.GetMaps(continentId, floorId, regionId);

        Assert.NotEmpty(actual);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(actual.Count, context.ResultContext.ResultCount);
        Assert.Equal(actual.Count, context.ResultContext.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                // TODO: complete validation
                entry.Has_id();
                entry.Has_name();
            }
        );
    }
}
