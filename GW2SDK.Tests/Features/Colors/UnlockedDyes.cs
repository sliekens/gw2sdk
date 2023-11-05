﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Colors;

public class UnlockedDyes
{
    [Fact]
    public async Task Unlocked_dyes_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Dyes.GetUnlockedDyesIndex(accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.NotEqual(0, id));
    }
}
