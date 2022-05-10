using System.Collections.Generic;
using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Armory;

public class ArmoryQueryTest
{
    [Fact]
    public async Task Legendary_items_can_be_enumerated()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Armory.GetLegendaryItems();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.True(entry.MaxCount > 0);
            }
        );
    }

    [Fact]
    public async Task Legendary_items_index_is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Armory.GetLegendaryItemsIndex();

        Assert.Equal(actual.Context.ResultTotal, actual.Count);
    }

    [Fact]
    public async Task A_legendary_item_can_be_found_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int legendaryItemId = 80111;

        var actual = await sut.Armory.GetLegendaryItemById(legendaryItemId);

        Assert.Equal(legendaryItemId, actual.Value.Id);
    }

    [Fact]
    public async Task Legendary_items_can_be_filtered_by_id()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            83162,
            93105,
            80111
        };

        var actual = await sut.Armory.GetLegendaryItemsByIds(ids);

        Assert.Collection(
            actual,
            first => Assert.Equal(83162, first.Id),
            second => Assert.Equal(93105, second.Id),
            third => Assert.Equal(80111, third.Id)
        );
    }

    [Fact]
    public async Task Legendary_items_can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Armory.GetLegendaryItemsByPage(0, 3);

        Assert.Equal(3, actual.Count);
        Assert.Equal(3, actual.Context.PageSize);
    }

    [Fact]
    public async Task Bound_legendary_items_can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();
        var accessToken = services.Resolve<ApiKey>();

        var actual = await sut.Armory.GetBoundLegendaryItems(accessToken.Key);

        Assert.NotEmpty(actual);
    }
}
