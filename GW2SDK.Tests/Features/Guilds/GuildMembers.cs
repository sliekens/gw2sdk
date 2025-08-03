using GuildWars2.Guilds.Members;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildMembers
{
    [Fact]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        TestGuildLeader guildLeader = TestConfiguration.TestGuildLeader;

        (AccountSummary account, _) = await sut.Hero.Account.GetSummary(
            guildLeader.Token,
            cancellationToken: TestContext.Current.CancellationToken
        );
        foreach (string guildId in account.LeaderOfGuildIds!)
        {
            (List<GuildMember> actual, _) = await sut.Guilds.GetGuildMembers(
                guildId,
                guildLeader.Token,
                cancellationToken: TestContext.Current.CancellationToken
            );

            Assert.NotEmpty(actual);
        }
    }
}
