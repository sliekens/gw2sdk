using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Minipets;

public class UnlockedMinipets
{
    [Fact]
    public async Task Can_be_enumerated()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Minipets.GetUnlockedMinipets(accessToken.Key);

        Assert.NotEmpty(actual.Value);
    }
}
