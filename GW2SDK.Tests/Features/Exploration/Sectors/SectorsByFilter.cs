﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Sectors;

public class SectorsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        HashSet<int> ids = new()
        {
            513,
            514,
            515
        };

        var actual = await sut.Maps.GetSectorsByIds(
            continentId,
            floorId,
            regionId,
            mapId,
            ids
        );

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, actual.Context.ResultCount);
        actual.All_have_ids();
        actual.Some_have_names();
        actual.All_have_chat_links();
    }
}