using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Home;

public class HomeQueryTest
{
    [Fact]
    public async Task Cats_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Home.GetCats();

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
        Assert.All(
            actual.Value,
            cat =>
            {
                Assert.NotNull(cat);
                Assert.True(cat.Id > 0);
                Assert.NotEmpty(cat.Hint);
            }
        );
    }

    [Fact]
    public async Task Cats_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var actual = await sut.Home.GetCatsIndex();
        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.ResultContext.ResultCount, actual.Value.Count);
    }

    [Fact]
    public async Task A_cat_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 20;

        var actual = await sut.Home.GetCatById(id);

        Assert.NotNull(actual.Value);
        Assert.Equal(20, actual.Value.Id);
        Assert.Equal("necromancer", actual.Value.Hint);
    }

    [Fact]
    public async Task Cats_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();
        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Home.GetCatsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Cats_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Home.GetCatsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
        Assert.All(
            actual.Value,
            cat =>
            {
                Assert.NotNull(cat);
                Assert.True(cat.Id > 0);
                Assert.NotEmpty(cat.Hint);
            }
        );
    }

    [Fact]
    public async Task Owned_cats_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var token = Composer.Resolve<ApiKey>();

        var actual = await sut.Home.GetOwnedCatsIndex(token.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, id => Assert.True(id > 0));
    }

    [Fact]
    public async Task Nodes_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Home.GetNodes();

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
        Assert.All(
            actual.Value,
            node =>
            {
                Assert.NotNull(node);
                Assert.NotEmpty(node.Id);
            }
        );
    }

    [Fact]
    public async Task Nodes_index_is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var actual = await sut.Home.GetNodesIndex();
        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.ResultContext.ResultCount, actual.Value.Count);
    }

    [Fact]
    public async Task Owned_nodes_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var token = Composer.Resolve<ApiKey>();

        var actual = await sut.Home.GetOwnedNodesIndex(token.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, id => Assert.NotEmpty(id));
    }
}
