﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Sectors;

public class SectorsIndex
{
    [Theory]
    [InlineData(1, 0, 1, 26)]
    [InlineData(1, 0, 1, 27)]
    [InlineData(1, 0, 1, 28)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId, int mapId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetSectorsIndex(continentId, floorId, regionId, mapId);

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Value.Count, actual.Context.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.Context.ResultContext.ResultTotal);
    }
}
