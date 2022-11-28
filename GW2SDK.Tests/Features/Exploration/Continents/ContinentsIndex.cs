using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Exploration.Continents;

public class ContinentsIndex
{
    [Fact]
    public async Task Is_not_empty()
    {
        await using Composer services = new();
        var sut = services.Resolve<Gw2Client>();

        var actual = await sut.Maps.GetContinentsIndex();

        Assert.Collection(actual, tyria => Assert.Equal(1, tyria), mists => Assert.Equal(2, mists));
        Assert.Equal(actual.Count, actual.Context.ResultCount);
        Assert.Equal(actual.Count, actual.Context.ResultTotal);
    }
}
