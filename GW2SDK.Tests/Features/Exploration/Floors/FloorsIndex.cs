﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Floors;

public class FloorsIndex
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    public async Task Is_not_empty(int continentId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetFloorsIndex(continentId);

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
    }
}
