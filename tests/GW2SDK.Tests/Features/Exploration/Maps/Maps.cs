using GuildWars2.Exploration.Maps;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Exploration.Maps;

public class Maps
{
    [Test]
    [Arguments(1, 0, 1)]
    [Arguments(1, 0, 2)]
    [Arguments(1, 0, 3)]
    public async Task Can_be_listed(int continentId, int floorId, int regionId)
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Map> actual, MessageContext context) = await sut.Exploration.GetMaps(continentId, floorId, regionId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, Assert.NotNull);
    }
}
