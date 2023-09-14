using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Armory;

public class LegendaryItemsIndex
{
    [Fact]
    public async Task Is_not_empty()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Armory.GetLegendaryItemsIndex();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }
}
