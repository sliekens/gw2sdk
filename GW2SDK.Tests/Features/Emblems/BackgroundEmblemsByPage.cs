﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Emblems;

public class BackgroundEmblemsByPage
{
    [Fact]
    public async Task Background_emblems_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Emblems.GetBackgroundEmblemsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.NotNull(context.PageContext);
        Assert.Equal(3, context.PageContext.PageSize);
    }
}
