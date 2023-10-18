using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildLog
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var guildLeader = Composer.Resolve<TestGuildLeader>();

        var account = await sut.Accounts.GetSummary(guildLeader.Token);
        foreach (var guildId in account.Value.LeaderOfGuildIds!)
        {
            var actual = await sut.Guilds.GetGuildLog(guildId, guildLeader.Token);

            Assert.NotEmpty(actual.Value);
        }
    }
}
