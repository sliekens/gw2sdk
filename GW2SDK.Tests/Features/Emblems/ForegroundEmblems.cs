﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Emblems;

public class ForegroundEmblems
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Emblems.GetForegroundEmblems();

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            emblem =>
            {
                Assert.True(emblem.Id > 0);
                Assert.NotEmpty(emblem.Layers);
                Assert.All(emblem.Layers, url => Assert.NotEmpty(url));
            }
        );
    }
}
