using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Mounts;

public class OwnedMountSkins
{
    [Fact]
    public async Task Owned_mount_skins_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Mounts.GetOwnedMountSkins(accessToken.Key);

        Assert.NotEmpty(actual);
    }

}
