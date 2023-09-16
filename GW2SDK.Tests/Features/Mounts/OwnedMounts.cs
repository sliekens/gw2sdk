using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Mounts;

public class OwnedMounts
{
    [Fact]
    public async Task Owned_mounts_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var actual = await sut.Mounts.GetOwnedMounts(accessToken.Key);

        Assert.NotEmpty(actual.Value);
    }
}
