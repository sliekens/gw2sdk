﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Regions;

public class RegionsIndex
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    public async Task Can_be_listed(int continentId, int floorId)
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();

        (HashSet<int> actual, MessageContext context) = await sut.Exploration.GetRegionsIndex(
            continentId,
            floorId,
            TestContext.Current.CancellationToken
        );

        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
