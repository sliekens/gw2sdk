using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Home;

public class HomeQueryTest
{
    [Fact]
    public async Task Cats_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<HomeQuery>();

        var actual = await sut.GetCats();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
        Assert.All(
            actual,
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
        await using Composer services = new();
        var sut = services.Resolve<HomeQuery>();
        var actual = await sut.GetCatsIndex();
        Assert.NotEmpty(actual);
        Assert.Equal(actual.Context.ResultCount, actual.Count);
    }

    [Fact]
    public async Task A_cat_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<HomeQuery>();
        var actual = await sut.GetCatById(20);
        Assert.NotNull(actual.Value);
        Assert.Equal(20, actual.Value.Id);
        Assert.Equal("necromancer", actual.Value.Hint);
    }

    [Fact]
    public async Task Cats_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<HomeQuery>();
        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.GetCatsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Equal(1, first.Id),
            second => Assert.Equal(2, second.Id),
            third => Assert.Equal(3, third.Id)
            );
    }

    [Fact]
    public async Task Cats_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<HomeQuery>();

        var actual = await sut.GetCatsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
        Assert.All(
            actual,
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
        await using Composer services = new();
        var sut = services.Resolve<HomeQuery>();
        var token = services.Resolve<ApiKey>();

        var actual = await sut.GetOwnedCatsIndex(token.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, id => Assert.True(id > 0));
    }

    [Fact]
    public async Task Nodes_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<HomeQuery>();

        var actual = await sut.GetNodes();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
        Assert.All(
            actual,
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
        await using Composer services = new();
        var sut = services.Resolve<HomeQuery>();
        var actual = await sut.GetNodesIndex();
        Assert.NotEmpty(actual);
        Assert.Equal(actual.Context.ResultCount, actual.Count);
    }

    [Fact]
    public async Task Owned_nodes_can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<HomeQuery>();
        var token = services.Resolve<ApiKey>();

        var actual = await sut.GetOwnedNodesIndex(token.Key);

        Assert.NotEmpty(actual.Value);
        Assert.All(actual.Value, id => Assert.NotEmpty(id));
    }
}
