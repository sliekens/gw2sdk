using GuildWars2.Exploration.Regions;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Regions;

[ServiceDataSource]
public class Regions(Gw2Client sut)
{
    [Test]
    [Arguments(1, 0)]
    [Arguments(2, 1)]
    public async Task Can_be_listed(int continentId, int floorId)
    {
        (HashSet<Region> actual, MessageContext context) = await sut.Exploration.GetRegions(continentId, floorId, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, Assert.NotNull);
    }
}
