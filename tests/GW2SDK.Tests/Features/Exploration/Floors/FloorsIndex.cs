using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Exploration.Floors;

[ServiceDataSource]
public class FloorsIndex(Gw2Client sut)
{
    [Test]
    [Arguments(1)]
    [Arguments(2)]
    public async Task Can_be_listed(int continentId)
    {
        (HashSet<int> actual, MessageContext context) = await sut.Exploration.GetFloorsIndex(continentId, TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
