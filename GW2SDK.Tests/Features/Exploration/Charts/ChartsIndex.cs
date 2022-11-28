using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Charts;

public class ChartsIndex
{
    [Theory]
    [InlineData(1, 0, 1)]
    [InlineData(1, 0, 2)]
    [InlineData(1, 0, 3)]
    public async Task Is_not_empty(int continentId, int floorId, int regionId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetChartsIndex(continentId, floorId, regionId);

        Assert.NotEmpty(actual.Values);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
    }
}
