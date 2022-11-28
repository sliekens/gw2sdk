using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.MapChests;

public class MapChestsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int pageSize = 3;
        var actual = await sut.MapChests.GetMapChestsByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Context.PageSize);
        Assert.Equal(pageSize, actual.Count);
        Assert.Equal(pageSize, actual.Context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
            }
        );
    }
}
