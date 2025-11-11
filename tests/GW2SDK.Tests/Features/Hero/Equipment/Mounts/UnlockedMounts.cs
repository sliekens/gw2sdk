using GuildWars2.Hero.Equipment.Mounts;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

[ServiceDataSource]
public class UnlockedMounts(Gw2Client sut)
{
    [Test]
    public async Task Unlocked_mounts_can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<Extensible<MountName>> actual, _) = await sut.Hero.Equipment.Mounts.GetUnlockedMounts(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        Assert.NotEmpty(actual);
        Assert.All(actual, entry =>
        {
            Assert.True(entry.IsDefined());
        });
    }
}
