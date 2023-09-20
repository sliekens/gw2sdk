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
