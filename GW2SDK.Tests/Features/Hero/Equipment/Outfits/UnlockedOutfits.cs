﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Outfits;

public class UnlockedOutfits
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = TestConfiguration.ApiKey;

        var (actual, _) = await sut.Hero.Equipment.Outfits.GetUnlockedOutfits(
            accessToken.Key,
            TestContext.Current.CancellationToken
        );

        Assert.NotEmpty(actual);
        Assert.All(actual, id => Assert.NotEqual(0, id));
    }
}
