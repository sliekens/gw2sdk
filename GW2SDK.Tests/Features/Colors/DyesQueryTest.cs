﻿using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Colors;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Colors;

public class DyesQueryTest
{
    private static class ColorFact
    {
        public static void Base_rgb_contains_red_green_blue(Dye actual) =>
            Assert.False(actual.BaseRgb.IsEmpty);
    }

    [Fact]
    public async Task Colors_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Dyes.GetColors();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            color =>
            {
                ColorFact.Base_rgb_contains_red_green_blue(color);
            }
        );
    }

    [Fact]
    public async Task Colors_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Dyes.GetColorsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_color_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int colorId = 1;

        var actual = await sut.Dyes.GetColorById(colorId);

        Assert.Equal(colorId, actual.Value.Id);
    }

    [Fact]
    public async Task Colors_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Dyes.GetColorsByIds(ids);

        Assert.Collection(
            actual.Value,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
        );
    }

    [Fact]
    public async Task Colors_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Dyes.GetColorsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Unlocked_dyes_can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.Dyes.GetUnlockedDyesIndex(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, id => Assert.NotEqual(0, id));
    }
}
