using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Emblems;

public class EmblemsQueryTest
{
    [Fact]
    public async Task Forground_emblems_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Emblems.GetForegroundEmblems();

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            emblem =>
            {
                Assert.True(emblem.Id > 0);
                Assert.NotEmpty(emblem.Layers);
                Assert.All(emblem.Layers, url => Assert.NotEmpty(url));
            }
        );
    }

    [Fact]
    public async Task Foreground_emblems_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Emblems.GetForegroundEmblemsIndex();

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_foreground_emblem_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Emblems.GetForegroundEmblemById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Foreground_emblems_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Emblems.GetForegroundEmblemsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.All(ids, id => Assert.Contains(id, actual.Value.Select(value => value.Id)));
    }

    [Fact]
    public async Task Foreground_emblems_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Emblems.GetForegroundEmblemsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Background_emblems_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Emblems.GetBackgroundEmblems();

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
            emblem =>
            {
                Assert.True(emblem.Id > 0);
                Assert.NotEmpty(emblem.Layers);
                Assert.All(emblem.Layers, url => Assert.NotEmpty(url));
            }
        );
    }

    [Fact]
    public async Task Background_emblems_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Emblems.GetBackgroundEmblemsIndex();

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_background_emblem_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 1;

        var actual = await sut.Emblems.GetBackgroundEmblemById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Background_emblems_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Emblems.GetBackgroundEmblemsByIds(ids);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.All(ids, id => Assert.Contains(id, actual.Value.Select(value => value.Id)));
    }

    [Fact]
    public async Task Background_emblems_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Emblems.GetBackgroundEmblemsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
