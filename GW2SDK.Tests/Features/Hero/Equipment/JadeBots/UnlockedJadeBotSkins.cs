using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Equipment.JadeBots;

public class UnlockedJadeBotSkins
{
    [Fact]
    public async Task Can_be_listed()
    {
        var sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<int> actual, _) = await sut.Hero.Equipment.JadeBots.GetUnlockedJadeBotSkins(
            accessToken.Key,
            TestContext.Current.CancellationToken
        );

        Assert.NotNull(actual);
    }
}
