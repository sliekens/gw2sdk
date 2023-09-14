using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Armory;

public class LegendaryItemsByPage
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Armory.GetLegendaryItemsByPage(0, 3);

        Assert.Equal(3, actual.Value.Count);
        Assert.Equal(3, actual.PageContext.PageSize);
    }
}
