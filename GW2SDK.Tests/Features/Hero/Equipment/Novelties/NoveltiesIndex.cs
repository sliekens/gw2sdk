﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Novelties;

public class NoveltiesIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Novelties.GetNoveltiesIndex();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, context.ResultCount);
        Assert.Equal(actual.Count, context.ResultTotal);
    }
}
