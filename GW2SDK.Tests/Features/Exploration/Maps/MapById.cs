﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapById
{
    [Theory]
    [InlineData(15)]
    [InlineData(17)]
    [InlineData(18)]
    public async Task Can_be_found(int id)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetMapById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
    }
}
