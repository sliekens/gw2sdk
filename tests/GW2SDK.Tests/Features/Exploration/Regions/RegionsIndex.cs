using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Regions;

[ServiceDataSource]
public class RegionsIndex(Gw2Client sut)
{
    [Test]
    [Arguments(1, 0)]
    [Arguments(2, 1)]
    public async Task Can_be_listed(int continentId, int floorId)
    {
        (HashSet<int> actual, MessageContext context) = await sut.Exploration.GetRegionsIndex(continentId, floorId, TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
