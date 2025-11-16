using GuildWars2.Pve.Home.Nodes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Nodes;

[ServiceDataSource]
public class Nodes(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        (HashSet<Node> actual, MessageContext context) = await sut.Pve.Home.GetNodes(cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotEmpty();
        await Assert.That(context).Member(c => c.ResultTotal, m => m.IsEqualTo(actual.Count));
        foreach (Node node in actual)
        {
            await Assert.That(node).IsNotNull();
            await Assert.That(node.Id).IsNotEmpty();
        }
    }
}
