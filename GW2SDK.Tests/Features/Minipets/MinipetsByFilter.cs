﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Minipets;

public class MinipetsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var (actual, context) = await sut.Minipets.GetMinipetsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(ids.Count, context.ResultContext.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_icon();
                entry.Has_order();
                entry.Has_item_id();
            }
        );
    }
}
