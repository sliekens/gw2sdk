using GuildWars2.Exploration.Hearts;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Hearts;

[ServiceDataSource]
public class HeartById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const int continentId = 1;
        const int floorId = 0;
        const int regionId = 1;
        const int mapId = 26;
        const int heartId = 2;
        (Heart actual, MessageContext context) = await sut.Exploration.GetHeartById(continentId, floorId, regionId, mapId, heartId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(heartId);
    }
}
