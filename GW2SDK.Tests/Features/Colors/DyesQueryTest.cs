using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Colors;

public class DyesQueryTest
{
    [Fact]
    public async Task Colors_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Dyes.GetColors();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            color =>
            {
                color.Base_rgb_contains_red_green_blue();
            }
        );
    }

    [Fact]
    public async Task Colors_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Dyes.GetColorsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_color_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Dyes.GetColorById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Colors_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Dyes.GetColorsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Colors_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Dyes.GetColorsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Unlocked_dyes_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Dyes.GetUnlockedDyesIndex(accessToken.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, id => Assert.NotEqual(0, id));
    }
}
