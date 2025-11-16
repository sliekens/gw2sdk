using GuildWars2.Quaggans;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Quaggans;

[ServiceDataSource]
public class QuaggansByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["404", "aloha", "attack"];
        (HashSet<Quaggan> actual, MessageContext context) = await sut.Quaggans.GetQuaggansByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).HasCount().EqualTo(ids.Count);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, c => c.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal!.Value).IsGreaterThan(ids.Count);
            await Assert.That(actual.ElementAt(0).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(1).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(2).Id).IsIn(ids);
        }
    }
}
