using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Equipment.JadeBots;

[ServiceDataSource]
public class UnlockedJadeBotSkins(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<int> actual, _) = await sut.Hero.Equipment.JadeBots.GetUnlockedJadeBotSkins(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual).IsNotNull();
    }
}
