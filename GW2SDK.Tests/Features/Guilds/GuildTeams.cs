using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildTeams
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var guildLeader = Composer.Resolve<TestGuildLeader>();

        var (account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token);
        foreach (var guildId in account.LeaderOfGuildIds!)
        {
            var (actual, _) = await sut.Guilds.GetGuildTeams(guildId, guildLeader.Token);

            Assert.NotNull(actual);
            Assert.All(actual,
                entry =>
                {
                    Assert.True(entry.Id > 0);
                    Assert.NotEmpty(entry.Name);
                    Assert.All(entry.Games,
                        game =>
                        {
                            Assert.True(game.RatingType.IsDefined());
                        });
                });
        }
    }
}
