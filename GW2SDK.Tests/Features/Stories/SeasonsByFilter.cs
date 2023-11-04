﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Stories;

public class SeasonsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<string> ids = new()
        {
            "09766A86-D88D-4DF2-9385-259E9A8CA583",
            "A515A1D3-4BD7-4594-AE30-2C5D05FF5960",
            "215AAA0F-CDAC-4F93-86DA-C155A99B5784"
        };

        var actual = await sut.Stories.GetSeasonsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.Context.ResultContext);
        Assert.Equal(ids.Count, actual.Context.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                entry.Has_id();
            }
        );
    }
}
