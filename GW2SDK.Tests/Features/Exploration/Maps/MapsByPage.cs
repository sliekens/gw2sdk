﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;

        var actual = await sut.Maps.GetMapsByPage(continentId, floorId, regionId, 0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.NotNull(actual.PageContext);
        Assert.Equal(3, actual.PageContext.PageSize);
        Assert.All(
            actual.Value,
            entry =>
            {
                // TODO: complete validation
                entry.Has_id();
                entry.Has_name();
            }
        );
    }
}
