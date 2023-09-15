using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Emblems;

public class ForegroundEmblemsIndex
{
    [Fact]
    public async Task Is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Emblems.GetForegroundEmblemsIndex();

        Assert.NotEmpty(actual.Value);
        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }
}
