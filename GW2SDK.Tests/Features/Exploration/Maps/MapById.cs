using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapById
{
    [Theory]
    [InlineData(15)]
    [InlineData(17)]
    [InlineData(18)]
    public async Task Can_be_found(int mapId)
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetMapById(mapId);

        Assert.Equal(mapId, actual.Value.Id);
        actual.Value.Has_name();
    }
}
