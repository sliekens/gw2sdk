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
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Items.GetItemsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }

    [Fact]
    public async Task An_item_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 24;

        var actual = await sut.Items.GetItemById(id);

        Assert.Equal(id, actual.Value.Id);
    }

    [Fact]
    public async Task Items_can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        HashSet<int> ids = new()
        {
            24,
            46,
            56
        };

        var actual = await sut.Items.GetItemsByIds(ids);

        Assert.Collection(
            ids,
            first => Assert.Contains(actual.Value, found => found.Id == first),
            second => Assert.Contains(actual.Value, found => found.Id == second),
            third => Assert.Contains(actual.Value, found => found.Id == third)
        );
    }

    [Fact]
    public async Task Items_can_be_filtered_by_page()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Items.GetItemsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }

    [Fact]
    public async Task Items_can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();

        // You wouldn't want to use Take() in production code
        //   but enumerating all entries is too expensive for a test
        // This code will actually try to fetch more than 600 entries
        //  but the extra requests will be cancelled when this test completes
        await foreach (var actual in sut.Items.GetItemsBulk(degreeOfParalllelism: 3).Take(600))
        {
            actual.Validate();
        }
    }
}
