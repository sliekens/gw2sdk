﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Home;

public class OwnedNodes
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var token = Composer.Resolve<ApiKey>();

        var actual = await sut.Home.GetOwnedNodesIndex(token.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, id => Assert.NotEmpty(id));
    }
}
