using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class UnlockedMounts
{
    [Fact]
    public async Task Unlocked_mounts_can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        var (actual, _) = await sut.Hero.Equipment.Mounts.GetUnlockedMounts(accessToken.Key);

        Assert.NotEmpty(actual);
        Assert.All(
            actual,
            entry =>
            {
                Assert.True(entry.IsDefined());
            }
        );
    }
}
