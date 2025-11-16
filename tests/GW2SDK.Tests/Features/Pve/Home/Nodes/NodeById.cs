using GuildWars2.Pve.Home.Nodes;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Pve.Home.Nodes;

[ServiceDataSource]
public class NodeById(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        const string id = "bauble_gathering_system";
        (Node actual, MessageContext context) = await sut.Pve.Home.GetNodeById(id, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context).IsNotNull();
        await Assert.That(actual.Id).IsEqualTo(id);
    }
}
