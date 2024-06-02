using GuildWars2.Guilds.Logs;
using GuildWars2.Http;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildLog
{
    [Fact(Skip = "API returns a gateway error")]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var guildLeader = TestConfiguration.TestGuildLeader;

        var (account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token);
        foreach (var guildId in account.LeaderOfGuildIds!)
        {
            var (actual, _) = await sut.Guilds.GetGuildLog(guildId, guildLeader.Token);

            Assert.NotEmpty(actual);
            Assert.All(
                actual,
                entry =>
                {
                    Assert.True(entry.Id > 0);
                    Assert.True(entry.Time > DateTimeOffset.MinValue);
                    switch (entry)
                    {
                        case GuildBankActivity guildBankActivity:
                            Assert.True(guildBankActivity.Operation.IsDefined());
                            break;
                        case GuildUpgradeActivity guildUpgradeActivity:
                            Assert.True(guildUpgradeActivity.Action.IsDefined());
                            break;
                        case InfluenceActivity influenceActivity:
                            Assert.True(influenceActivity.Activity.IsDefined());
                            break;
                    }
                }
            );

            // While we are here, check the ability to use a log ID as a skip token
            if (actual.Count > 3)
            {
                var skipToken = actual[3].Id;
                var (range, _) = await sut.Guilds.GetGuildLog(
                    guildId,
                    skipToken,
                    guildLeader.Token
                );
                Assert.True(range.Count >= 3);
                Assert.All(range, log => Assert.True(log.Id > skipToken));
            }
        }
    }

    // Test to prove that the API is misbehaving and the test above should be skipped
    // When this test is removed, the test above should be re-enabled by removing the Skip attribute
    [Fact]
    public async Task Returns_Gateway_Error()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var guildLeader = TestConfiguration.TestGuildLeader;

        var account = await sut.Hero.Account.GetSummary(guildLeader.Token).ValueOnly();

        Exception? error = null;
        foreach (var guildId in account.LeaderOfGuildIds!)
        {
            error = await Record.ExceptionAsync(
                async () => await sut.Guilds.GetGuildLog(guildId, guildLeader.Token)
            );

            if (error is not null) break;
        }

        var badResponse = Assert.IsType<BadResponseException>(error);
        Assert.True(badResponse.Message is "Gateway Time-out" or "Bad Gateway");
    }
}
