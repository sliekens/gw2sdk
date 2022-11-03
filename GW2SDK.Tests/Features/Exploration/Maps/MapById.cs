using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Maps;

public class MapById
{
    [Theory]
    [InlineData(15)]
    [InlineData(17)]
    [InlineData(18)]
    public async Task Can_be_found(int mapId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetMapById(mapId);

        Assert.Equal(mapId, actual.Value.Id);
        actual.Value.Has_name();
    }
}
