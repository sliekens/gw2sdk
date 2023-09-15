using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Home;

public class NodesIndex
{
    [Fact]
    public async Task Is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var actual = await sut.Home.GetNodesIndex();
        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.ResultContext.ResultCount, actual.Value.Count);
    }
}
