using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Items;

public class ItemsQueryTest
{
    [Fact]
    public async Task Items_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Items.GetItemsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task An_item_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int itemId = 24;

        var actual = await sut.Items.GetItemById(itemId);

        Assert.Equal(itemId, actual.Value.Id);
    }

    [Fact]
    public async Task Items_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            24,
            46,
            56
        };

        var actual = await sut.Items.GetItemsByIds(ids).ToListAsync();

        Assert.Collection(
            actual,
            first => Assert.Contains(first.Id, ids),
            second => Assert.Contains(second.Id, ids),
            third => Assert.Contains(third.Id, ids)
        );
    }

    [Fact]
    public async Task Items_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Items.GetItemsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact(
        Skip =
            "This test is best used interactively, otherwise it will hit rate limits in this as well as other tests."
    )]
    public async Task Items_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        await foreach (var actual in sut.Items.GetItems())
        {
            ItemFacts.Validate(actual);
        }
    }
}
