using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildTeams
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var guildLeader = TestConfiguration.TestGuildLeader;

        var (account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token);
        foreach (var guildId in account.LeaderOfGuildIds!)
        {
            var (actual, _) = await sut.Guilds.GetGuildTeams(guildId, guildLeader.Token);

            Assert.NotNull(actual);
            Assert.All(
                actual,
                entry =>
                {
                    Assert.True(entry.Id > 0);
                    Assert.True(entry.State.IsDefined());
                    Assert.NotEmpty(entry.Name);
                    Assert.All(
                        entry.Members,
                        member =>
                        {
                            Assert.NotEmpty(member.Name);
                            Assert.True(member.Role.IsDefined());
                        }
                    );
                    Assert.All(
                        entry.Games,
                        game =>
                        {
                            Assert.True(game.Result.IsDefined());
                            Assert.True(game.Team.IsDefined());
                            Assert.True(game.RatingType.IsDefined());
                        }
                    );
                }
            );
        }
    }
}
