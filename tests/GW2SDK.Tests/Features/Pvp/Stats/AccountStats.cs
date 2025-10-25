using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Pvp.Stats;

public class AccountStats
{
    [Test]
    public async Task Can_be_listed()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Pvp.Stats.AccountStats actual, _) = await sut.Pvp.GetStats(accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);
        Assert.NotNull(actual);
    }
}
