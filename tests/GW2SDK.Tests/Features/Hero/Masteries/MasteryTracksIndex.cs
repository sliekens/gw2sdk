using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Masteries;

[ServiceDataSource]
public class MasteryTracksIndex(Gw2Client sut)
{
    [Test]
    public async Task Masteries_index_Can_be_listed()
    {
        (HashSet<int> actual, MessageContext context) = await sut.Hero.Masteries.GetMasteryTracksIndex(TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
    }
}
