﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Stats;

public class AccountStats
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Pvp.GetStats(
            accessToken.Key,
            cancellationToken: TestContext.Current.CancellationToken
        );

        Assert.NotNull(actual);
    }
}
