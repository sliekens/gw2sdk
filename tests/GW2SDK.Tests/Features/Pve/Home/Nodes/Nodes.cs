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
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, node =>
        {
            Assert.NotNull(node);
            Assert.NotEmpty(node.Id);
        });
    }
}
