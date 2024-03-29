﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Upgrades;

public class UpgradesByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids =
        [
            1, 3,
            4
        ];

        var (actual, context) = await sut.Wvw.GetUpgradesByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_tiers();
            }
        );
    }
}
