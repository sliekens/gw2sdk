﻿using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

public class MatchOverviewByWorldId
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int worldId = 2006;

        var actual = await sut.Wvw.GetMatchOverviewByWorldId(worldId);

        actual.Value.Has_id();
        actual.Value.Has_start_time();
        actual.Value.Has_end_time();
        actual.Value.Includes_world(worldId);
    }
}
