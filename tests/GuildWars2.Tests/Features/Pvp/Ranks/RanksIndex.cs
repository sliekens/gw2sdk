using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pvp.Ranks;

[ServiceDataSource]
public class RanksIndex(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<int> actual, MessageContext context) = await sut.Pvp.GetRanksIndex(TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
            await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
            await Assert.That(actual).IsNotEmpty();
        }
    }
}
