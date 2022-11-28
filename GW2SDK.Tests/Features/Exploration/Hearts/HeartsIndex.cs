using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Hearts;

public class HeartsIndex
{
    [Theory]
    [InlineData(1, 0, 1, 26)]
    [InlineData(1, 0, 1, 27)]
    [InlineData(1, 0, 1, 28)]
    public async Task Is_not_empty(int continentId, int floorId, int regionId, int mapId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetHeartsIndex(continentId, floorId, regionId, mapId);

        Assert.NotEmpty(actual.Values);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
    }
}
