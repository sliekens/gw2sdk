using GuildWars2.Guilds.Ranks;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildRanks
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        TestGuildLeader guildLeader = TestConfiguration.TestGuildLeader;

        (AccountSummary account, _) = await sut.Hero.Account.GetSummary(
            guildLeader.Token,
            cancellationToken: TestContext.Current.CancellationToken
        );
        foreach (var guildId in account.LeaderOfGuildIds!)
        {
            (List<GuildRank> actual, _) = await sut.Guilds.GetGuildRanks(
                guildId,
                guildLeader.Token,
                cancellationToken: TestContext.Current.CancellationToken
            );

            Assert.NotEmpty(actual);
        }
    }
}
