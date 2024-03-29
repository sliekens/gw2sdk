﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();
        var guild = Composer.Resolve<TestGuild>();

        var (actual, _) = await sut.Guilds.GetGuildById(guild.Id, accessToken.Key);

        Assert.NotNull(actual);
        Assert.Equal(guild.Id, actual.Id);
        Assert.Equal(guild.Name, actual.Name);
        Assert.Equal(guild.Tag, actual.Tag);
    }
}
