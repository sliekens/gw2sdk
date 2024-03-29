﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches.Overview;

public class MatchesOverviewByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids =
        [
            "1-1", "1-2",
            "1-3"
        ];

        var (actual, context) = await sut.Wvw.GetMatchesOverviewByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_start_time();
                entry.Has_end_time();
            }
        );
    }
}
