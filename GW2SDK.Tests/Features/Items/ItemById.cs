using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Items;

public class ItemById
{
    [Fact]
    public async Task An_item_can_be_found_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int id = 24;

        var actual = await sut.Items.GetItemById(id);

        Assert.Equal(id, actual.Value.Id);
    }
}
