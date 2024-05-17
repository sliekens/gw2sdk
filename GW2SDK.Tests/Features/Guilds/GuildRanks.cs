using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildRanks
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var guildLeader = TestConfiguration.TestGuildLeader;

        var (account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token);
        foreach (var guildId in account.LeaderOfGuildIds!)
        {
            var (actual, _) = await sut.Guilds.GetGuildRanks(guildId, guildLeader.Token);

            Assert.NotEmpty(actual);
        }
    }
}
