﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.ItemStats;

public class ItemStatById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 559;

        var (actual, _) = await sut.ItemStats.GetItemStatById(id);

        Assert.Equal(id, actual.Id);
    }
}
