﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Mounts;

public class MountSkinsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountSkinsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.NotNull(actual.PageContext);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
