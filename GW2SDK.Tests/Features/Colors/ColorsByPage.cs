﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Colors;

public class ColorsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Dyes.GetColorsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.NotNull(actual.Context.PageContext);
        Assert.Equal(3, actual.Context.PageContext.PageSize);
    }
}
