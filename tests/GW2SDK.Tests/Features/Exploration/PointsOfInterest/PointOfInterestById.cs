using GuildWars2.Exploration.PointsOfInterest;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.PointsOfInterest;

[ServiceDataSource]
public class PointOfInterestById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        const int pointOfInterestId = 554;
        (PointOfInterest actual, MessageContext context) = await sut.Exploration.GetPointOfInterestById(continentId, floorId, regionId, mapId, pointOfInterestId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(pointOfInterestId, actual.Id);
        Assert.Equal("Leaning Grade", actual.Name);
        Assert.Equal(1, actual.Floor);
        Assert.Equal(52657.7f, actual.Coordinates.X);
        Assert.Equal(32978.8f, actual.Coordinates.Y);
        Assert.Equal("[&BCoCAAA=]", actual.ChatLink);
    }
}
