﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.PointsOfInterest;

public class PointsOfInterestByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;

        var (actual, context) = await sut.Exploration.GetPointsOfInterestByPage(
            continentId,
            floorId,
            regionId,
            mapId,
            0,
            3
        );

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, context.PageSize);
        actual.All_have_ids();
        actual.Some_have_names();
        actual.All_have_chat_links();
    }
}
