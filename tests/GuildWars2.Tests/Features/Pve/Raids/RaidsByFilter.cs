using GuildWars2.Pve.Raids;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Raids;

[ServiceDataSource]
public class RaidsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["forsaken_thicket", "bastion_of_the_penitent", "hall_of_chains"];
        (IImmutableValueSet<Raid> actual, MessageContext context) = await sut.Pve.Raids.GetRaidsByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        using (Assert.Multiple())
        {
            await Assert.That(context).Member(c => c.ResultCount, rc => rc.IsEqualTo(ids.Count));
            await Assert.That(context.ResultTotal > ids.Count).IsTrue();
            await Assert.That(actual).Count().IsEqualTo(ids.Count);
            foreach (string id in ids)
            {
                await Assert.That(actual).Contains(found => found.Id == id);
            }
        }
    }
}
