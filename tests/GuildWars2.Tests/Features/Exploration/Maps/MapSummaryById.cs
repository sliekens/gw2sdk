using GuildWars2.Exploration.Maps;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Maps;

[ServiceDataSource]
public class MapSummaryById(Gw2Client sut)
{
    [Test]
    [Arguments(15)]
    [Arguments(17)]
    [Arguments(18)]
    public async Task Can_be_found(int id)
    {
        (MapSummary actual, MessageContext context) = await sut.Exploration.GetMapSummaryById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
