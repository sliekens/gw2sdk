using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Armory;

public class BoundLegendaryItems
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Armory.GetBoundLegendaryItems(accessToken.Key);

        Assert.NotEmpty(actual.Value);
    }
}
