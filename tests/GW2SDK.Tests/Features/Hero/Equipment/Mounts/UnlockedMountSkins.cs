using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class UnlockedMountSkins
{
    [Test]
    public async Task Unlocked_mount_skins_can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Hero.Equipment.Mounts.GetUnlockedMountSkins(accessToken.Key, TestContext.Current!.CancellationToken);
        Assert.NotEmpty(actual);
    }
}
