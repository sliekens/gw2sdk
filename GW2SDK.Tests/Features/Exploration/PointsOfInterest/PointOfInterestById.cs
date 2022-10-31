using System.Threading.Tasks;
using GW2SDK.Tests.TestInfrastructure;
using Xunit;

namespace GW2SDK.Tests.Features.Exploration.PointsOfInterest;

public class PointOfInterestById
{
    [Fact]
    public async Task Can_be_found()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        const int pointOfInterestId = 554;

        var actual = await sut.Maps.GetPointOfInterestById(
            continentId,
            floorId,
            regionId,
            mapId,
            pointOfInterestId
        );

        Assert.Equal(pointOfInterestId, actual.Value.Id);
        Assert.Equal("Leaning Grade", actual.Value.Name);
        Assert.Equal(1, actual.Value.Floor);
        Assert.Equal(52657.7f, actual.Value.Coordinates.X);
        Assert.Equal(32978.8f, actual.Value.Coordinates.Y);
        Assert.Equal("[&BCoCAAA=]", actual.Value.ChatLink);
    }
}
