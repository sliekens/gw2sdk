using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Home;

public class Nodes
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Home.GetNodes();

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
        Assert.All(
            actual.Value,
            node =>
            {
                Assert.NotNull(node);
                Assert.NotEmpty(node.Id);
            }
        );
    }
}
