using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Pvp.Amulets;

public class AmuletById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int id = 4;

        var actual = await sut.Pvp.GetAmuletById(id);

        Assert.Equal(id, actual.Value.Id);
        actual.Value.Has_name();
        actual.Value.Has_icon();
        actual.Value.Has_attributes();
    }
}
