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
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(pointOfInterestId);
        await Assert.That(actual.Name).IsEqualTo("Leaning Grade");
        await Assert.That(actual.Floor).IsEqualTo(1);
        await Assert.That(actual.Coordinates.X).IsEqualTo(52657.7f);
        await Assert.That(actual.Coordinates.Y).IsEqualTo(32978.8f);
        await Assert.That(actual.ChatLink).IsEqualTo("[&BCoCAAA=]");
    }
}
