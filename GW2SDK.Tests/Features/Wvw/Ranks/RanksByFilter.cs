﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Ranks;

public class RanksByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids =
        [
            1, 2,
            3
        ];

        var (actual, context) = await sut.Wvw.GetRanksByIds(ids);

        Assert.Equal(ids.Count, context.ResultCount);
        Assert.True(context.ResultTotal > ids.Count);
        Assert.Equal(ids.Count, actual.Count);
        Assert.Collection(
            ids,
            first => Assert.Contains(actual, found => found.Id == first),
            second => Assert.Contains(actual, found => found.Id == second),
            third => Assert.Contains(actual, found => found.Id == third)
        );
    }
}
