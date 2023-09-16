using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Worlds;

public class WorldsIndex
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Worlds.GetWorldsIndex();

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultCount);
        Assert.Equal(actual.Value.Count, actual.ResultContext.ResultTotal);
    }
}