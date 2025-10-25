using GuildWars2.Pve.Home.Nodes;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home.Nodes;

public class Nodes
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        (HashSet<Node> actual, MessageContext context) = await sut.Pve.Home.GetNodes(cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.All(actual, node =>
        {
            Assert.NotNull(node);
            Assert.NotEmpty(node.Id);
        });
    }
}
