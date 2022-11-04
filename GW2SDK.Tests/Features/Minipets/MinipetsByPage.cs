using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Minipets;

public class MinipetsByPage
{
    [Fact]
    public async Task Can_be_filtered_by_page()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int pageSize = 3;
        var actual = await sut.Minipets.GetMinipetsByPage(0, pageSize);

        Assert.Equal(pageSize, actual.Count);
        Assert.Equal(pageSize, actual.Context.PageSize);
        Assert.Equal(pageSize, actual.Context.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                entry.Has_id();
                entry.Has_name();
                entry.Has_icon();
                entry.Has_order();
                entry.Has_item_id();
            }
        );
    }
}
