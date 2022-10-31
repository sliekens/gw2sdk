using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.Regions;

public class RegionsIndex
{
    [Theory]
    [InlineData(1, 0)]
    [InlineData(2, 1)]
    public async Task Is_not_empty(int continentId, int floorId)
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetRegionsIndex(continentId, floorId);

        Assert.NotEmpty(actual.Values);
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
    }
}
