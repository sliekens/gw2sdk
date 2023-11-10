using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class OwnedMounts
{
    [Fact]
    public async Task Owned_mounts_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Equipment.Mounts.GetOwnedMounts(accessToken.Key);

        Assert.NotEmpty(actual);
    }
}
