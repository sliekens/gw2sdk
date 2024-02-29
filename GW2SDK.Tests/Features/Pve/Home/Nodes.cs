using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pve.Home;

public class Nodes
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var (actual, context) = await sut.Pve.Home.GetNodes();

        Assert.NotEmpty(actual);
        Assert.Equal(actual.Count, context.ResultTotal);
        Assert.All(
            actual,
            node =>
            {
                Assert.NotNull(node);
                Assert.NotEmpty(node.Id);
            }
        );
    }
}
