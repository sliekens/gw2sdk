﻿using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Hearts;

public class HeartById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        const int heartId = 2;

        var actual = await sut.Maps.GetHeartById(
            continentId,
            floorId,
            regionId,
            mapId,
            heartId
        );

        Assert.Equal(heartId, actual.Value.Id);
    }
}