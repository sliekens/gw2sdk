﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home;

public class OwnedCats
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var token = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Pve.Home.GetOwnedCatsIndex(token.Key);

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.True(id > 0));
    }
}