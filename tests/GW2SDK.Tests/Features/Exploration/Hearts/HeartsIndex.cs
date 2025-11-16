using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Hearts;

[ServiceDataSource]
public class HeartsIndex(Gw2Client sut)
{
    [Test]
    [Arguments(1, 0, 1, 26)]
    [Arguments(1, 0, 1, 27)]
    [Arguments(1, 0, 1, 28)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId, int mapId)
    {
        (HashSet<int> actual, MessageContext context) = await sut.Exploration.GetHeartsIndex(continentId, floorId, regionId, mapId, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
    }
}
