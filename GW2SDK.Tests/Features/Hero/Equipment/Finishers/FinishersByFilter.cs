﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Finishers;

public class FinishersByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids =
        [
            1, 38,
            74
        ];

        var (actual, context) = await sut.Hero.Equipment.Finishers.GetFinishersByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_unlock_details();
                entry.Has_unlock_items();
                entry.Has_order();
                entry.Has_icon();
                entry.Has_name();
            }
        );
    }
}
