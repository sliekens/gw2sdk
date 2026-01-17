using GuildWars2.Pve.Home.Cats;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Cats;

[ServiceDataSource]
public class CatsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [1, 2, 3];
        (IImmutableValueSet<Cat> actual, MessageContext context) = await sut.Pve.Home.GetCatsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal > ids.Count).IsTrue();
            await Assert.That(actual).Count().IsEqualTo(ids.Count);
            await Assert.That(actual.ElementAt(0).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(1).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(2).Id).IsIn(ids);
        }
    }
}
