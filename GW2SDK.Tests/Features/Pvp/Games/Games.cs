﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Games;

public class Games
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Pvp.GetGames(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(actual.Value.Count, actual.Context.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.Context.ResultContext.ResultTotal);
    }
}
