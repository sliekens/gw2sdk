﻿using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Dyes;

public class Colors
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Hero.Equipment.Dyes.GetColors();

        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            color =>
            {
                color.Base_rgb_contains_red_green_blue();
            }
        );
    }
}
