using GuildWars2.Hero.Accounts;

using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Guilds;

public class CompletedGuildUpgrades
{

    [Test]

    public async Task Can_be_found()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        TestGuildLeader guildLeader = TestConfiguration.TestGuildLeader;

        (AccountSummary account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token, cancellationToken: TestContext.Current!.CancellationToken);

        foreach (string? guildId in account.LeaderOfGuildIds!)
        {

            (HashSet<int> actual, _) = await sut.Guilds.GetCompletedGuildUpgrades(guildId, guildLeader.Token, TestContext.Current!.CancellationToken);

            Assert.NotEmpty(actual);
        }
    }
}
