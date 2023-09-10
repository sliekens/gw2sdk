using System.Collections.Generic;
using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Armory;

public class ArmoryQueryTest
{
    [Fact]
    public async Task Legendary_items_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Armory.GetLegendaryItems();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
        Assert.All(
            actual.Value,
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
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Armory.GetLegendaryItemsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task A_legendary_item_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 80111;

        var actual = await sut.Armory.GetLegendaryItemById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Legendary_items_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            83162,
            93105,
            80111
        };

        var actual = await sut.Armory.GetLegendaryItemsByIds(ids);

        Assert.Collection(
            actual.Value,
            first => Assert.Equal(83162, first.Id),
            second => Assert.Equal(93105, second.Id),
            third => Assert.Equal(80111, third.Id)
        );
    }

    [Fact]
    public async Task Legendary_items_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Armory.GetLegendaryItemsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Bound_legendary_items_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Armory.GetBoundLegendaryItems(accessToken.Key);

        Assert.NotEmpty(actual.Value);
    }
}
