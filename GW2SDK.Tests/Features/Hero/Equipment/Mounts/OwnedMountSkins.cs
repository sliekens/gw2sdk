﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class UnlockedMountSkins
{
    [Fact]
    public async Task Unlocked_mount_skins_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Equipment.Mounts.GetUnlockedMountSkins(accessToken.Key);

        Assert.NotEmpty(actual);
    }
}
