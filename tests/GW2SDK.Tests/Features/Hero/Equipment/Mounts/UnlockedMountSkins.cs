using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class UnlockedMountSkins(Gw2Client sut)
{
    [Test]
    public async Task Unlocked_mount_skins_can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Hero.Equipment.Mounts.GetUnlockedMountSkins(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
    }
}
