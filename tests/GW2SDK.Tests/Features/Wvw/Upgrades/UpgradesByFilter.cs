using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Wvw.Upgrades;

namespace GuildWars2.Tests.Features.Wvw.Upgrades;

[ServiceDataSource]
public class UpgradesByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<int> ids = [1, 3, 4];
        (HashSet<ObjectiveUpgrade> actual, MessageContext context) = await sut.Wvw.GetUpgradesByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal > ids.Count).IsTrue();
            await Assert.That(actual).HasCount(ids.Count);
            await Assert.That(actual.ElementAt(0).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(1).Id).IsIn(ids);
            await Assert.That(actual.ElementAt(2).Id).IsIn(ids);
        }
    }
}
