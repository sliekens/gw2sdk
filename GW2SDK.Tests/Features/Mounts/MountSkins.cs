using System.Threading.Tasks;
using GuildWars2.Tests.TestInfrastructure;
using Xunit;

namespace GuildWars2.Tests.Features.Mounts;

public class MountSkins
{
    [Fact]
    public async Task Mount_skins_can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();

        var actual = await sut.Mounts.GetMountSkins();

        Assert.Equal(actual.ResultContext.ResultTotal, actual.Value.Count);
    }
}
