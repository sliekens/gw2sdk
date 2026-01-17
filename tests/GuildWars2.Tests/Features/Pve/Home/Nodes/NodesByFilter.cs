using GuildWars2.Pve.Home.Nodes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Nodes;

[ServiceDataSource]
public class NodesByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        HashSet<string> ids = ["garden_plot_01", "garden_plot_02", "garden_plot_03"];
        (IImmutableValueSet<Node> actual, MessageContext context) = await sut.Pve.Home.GetNodesByIds(ids, cancellationToken: TestContext.Current!.Execution.CancellationToken);
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
