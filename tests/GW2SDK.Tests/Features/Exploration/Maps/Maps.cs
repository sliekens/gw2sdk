using GuildWars2.Exploration.Maps;
using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Maps;

[ServiceDataSource]
public class Maps(Gw2Client sut)
{
    [Test]
    [Arguments(1, 0, 1)]
    [Arguments(1, 0, 2)]
    [Arguments(1, 0, 3)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId)
    {
        (HashSet<Map> actual, MessageContext context) = await sut.Exploration.GetMaps(continentId, floorId, regionId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, Assert.NotNull);
    }
}
