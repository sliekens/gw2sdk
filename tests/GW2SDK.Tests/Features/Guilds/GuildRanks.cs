using GuildWars2.Guilds.Ranks;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds;

[ServiceDataSource]
public class GuildRanks(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestGuildLeader guildLeader = TestConfiguration.TestGuildLeader;
        (AccountSummary account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        foreach (string? guildId in account.LeaderOfGuildIds!)
        {
            (List<GuildRank> actual, _) = await sut.Guilds.GetGuildRanks(guildId, guildLeader.Token, cancellationToken: TestContext.Current!.Execution.CancellationToken);
            Assert.NotEmpty(actual);
        }
    }
}
