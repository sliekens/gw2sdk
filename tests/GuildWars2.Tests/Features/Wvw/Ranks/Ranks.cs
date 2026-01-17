using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Ranks;

namespace GuildWars2.Tests.Features.Wvw.Ranks;

[ServiceDataSource]
public class Ranks(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (IImmutableValueSet<Rank> actual, MessageContext context) = await sut.Wvw.GetRanks(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultCount, m => m.IsEqualTo(actual.Count));
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (Rank entry in actual)
        {
            await Assert.That(entry.Id > 0).IsTrue();
            await Assert.That(entry.Title).IsNotEmpty();
            await Assert.That(entry.MinRank > 0).IsTrue();
        }
    }
}
