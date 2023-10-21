﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Seasons;

public class SeasonsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "44B85826-B5ED-4890-8C77-82DDF9F2CF2B",
            "95D5B290-798A-421E-A919-1C2A75F74B72",
            "D1777261-555B-4B72-A27E-BDC96EC393D5"
        };

        var actual = await sut.Pvp.GetSeasonsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
            }
        );
    }
}
