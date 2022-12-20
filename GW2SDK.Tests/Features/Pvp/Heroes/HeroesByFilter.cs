﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Pvp.Heroes;

public class HeroesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "115C140F-C2F5-40EB-8EA2-C3773F2AE468",
            "B7EA9889-5F16-4636-9705-4FCAF8B39ECD",
            "BEA79596-CA8B-4D46-9B9C-EA1B606BCF42"
        };

        var actual = await sut.Pvp.GetHeroesByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(ids.Count, actual.Context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
            }
        );
    }
}