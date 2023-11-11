using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.PointsOfInterest;

public class PointOfInterestById
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();

        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        const int pointOfInterestId = 554;

        var (actual, _) = await sut.Exploration.GetPointOfInterestById(
            continentId,
            floorId,
            regionId,
            mapId,
            pointOfInterestId
        );

        Assert.Equal(pointOfInterestId, actual.Id);
        Assert.Equal("Leaning Grade", actual.Name);
        Assert.Equal(1, actual.Floor);
        Assert.Equal(52657.7f, actual.Coordinates.X);
        Assert.Equal(32978.8f, actual.Coordinates.Y);
        Assert.Equal("[&BCoCAAA=]", actual.ChatLink);
    }
}
