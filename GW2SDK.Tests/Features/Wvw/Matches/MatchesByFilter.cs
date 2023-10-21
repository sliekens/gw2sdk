﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Matches;

public class MatchesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "1-1",
            "1-2",
            "1-3"
        };

        var actual = await sut.Wvw.GetMatchesByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_start_time();
                entry.Has_end_time();
            }
        );
    }
}
