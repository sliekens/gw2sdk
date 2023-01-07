﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Charts;

public class ChartsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        HashSet<int> ids = new()
        {
            26,
            27,
            28
        };

        var actual = await sut.Maps.GetChartsByIds(continentId, floorId, regionId, ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
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
