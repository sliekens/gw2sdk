using GuildWars2.Guilds.Bank;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Guilds;

[ServiceDataSource]
public class GuildBank(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestGuildLeader guildLeader = TestConfiguration.TestGuildLeader;
        (AccountSummary account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        foreach (string guildId in account.LeaderOfGuildIds!)
        {
            (IImmutableValueList<GuildBankTab> actual, _) = await sut.Guilds.GetGuildBank(guildId, guildLeader.Token, cancellationToken: TestContext.Current!.Execution.CancellationToken);
            await Assert.That(actual).IsNotNull();
        }
    }
}
