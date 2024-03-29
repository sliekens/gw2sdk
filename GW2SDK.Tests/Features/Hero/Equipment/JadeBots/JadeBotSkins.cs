﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.JadeBots;

public class JadeBotSkins
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.JadeBots.GetJadeBotSkins();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_description();
                entry.Has_unlock_item();
            }
        );
    }
}
