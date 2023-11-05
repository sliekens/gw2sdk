﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Stats;

public class MatchesStats
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Wvw.GetMatchesStats();

        Assert.NotEmpty(actual);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(actual.Count, context.ResultContext.ResultCount);
        Assert.Equal(actual.Count, context.ResultContext.ResultTotal);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_kills();
                entry.Has_deaths();
                entry.Has_maps();
            }
        );
    }
}
