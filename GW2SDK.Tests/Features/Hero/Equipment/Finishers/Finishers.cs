﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Finishers;

public class Finishers
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Finishers.GetFinishers();

        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_unlock_details();
                entry.Has_unlock_items();
                entry.Has_order();
                entry.Has_icon();
                entry.Has_name();
            }
        );
    }
}
