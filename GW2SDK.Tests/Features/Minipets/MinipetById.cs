using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Minipets;

public class MinipetById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int minipetId = 1;

        var actual = await sut.Minipets.GetMinipetById(minipetId);

        Assert.Equal(minipetId, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_icon();
        actual.Value.Has_order();
        actual.Value.Has_item_id();
    }
}
