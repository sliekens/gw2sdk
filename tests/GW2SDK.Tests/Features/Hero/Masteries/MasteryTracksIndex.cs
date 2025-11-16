using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Hero.Masteries;

[ServiceDataSource]
public class MasteryTracksIndex(Gw2Client sut)
{
    [Test]
    public async Task Masteries_index_Can_be_listed()
    {
        (HashSet<int> actual, MessageContext context) = await sut.Hero.Masteries.GetMasteryTracksIndex(TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
    }
}
