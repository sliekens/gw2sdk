using GuildWars2.Hero.Equipment.Mounts;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Equipment.Mounts;

public class UnlockedMounts
{

    [Test]

    public async Task Unlocked_mounts_can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<Extensible<MountName>> actual, _) = await sut.Hero.Equipment.Mounts.GetUnlockedMounts(accessToken.Key, TestContext.Current!.CancellationToken);

        Assert.NotEmpty(actual);

        Assert.All(actual, entry =>
        {
            Assert.True(entry.IsDefined());
        });
    }
}
