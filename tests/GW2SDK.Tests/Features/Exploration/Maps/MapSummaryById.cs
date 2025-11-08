using GuildWars2.Exploration.Maps;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class MapSummaryById
{
    [Test]
    [Arguments(15)]
    [Arguments(17)]
    [Arguments(18)]
    public async Task Can_be_found(int id)
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (MapSummary actual, MessageContext context) = await sut.Exploration.GetMapSummaryById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context);
        Assert.Equal(id, actual.Id);
    }
}
