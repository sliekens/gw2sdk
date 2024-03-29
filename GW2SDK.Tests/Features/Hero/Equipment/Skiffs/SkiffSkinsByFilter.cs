﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Skiffs;

public class SkiffSkinsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids =
        [
            410, 413,
            420
        ];

        var (actual, context) = await sut.Hero.Equipment.Skiffs.GetSkiffSkinsByIds(ids);

        Assert.Equal(ids.Count, actual.Count);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_icon();
                entry.Has_dye_slots();
            }
        );
    }
}
