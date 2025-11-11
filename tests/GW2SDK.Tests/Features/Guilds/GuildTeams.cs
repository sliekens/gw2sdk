using GuildWars2.Guilds.Teams;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;
using GuildWars2.Tests.TestInfrastructure.Composition;

namespace GuildWars2.Tests.Features.Guilds;

[ServiceDataSource]
public class GuildTeams(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestGuildLeader guildLeader = TestConfiguration.TestGuildLeader;
        (AccountSummary account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        foreach (string guildId in account.LeaderOfGuildIds!)
        {
            (List<GuildTeam> actual, _) = await sut.Guilds.GetGuildTeams(guildId, guildLeader.Token, cancellationToken: TestContext.Current!.Execution.CancellationToken);
            Assert.NotNull(actual);
            Assert.All(actual, entry =>
            {
                Assert.True(entry.Id > 0);
                Assert.True(entry.State.IsDefined());
                Assert.NotEmpty(entry.Name);
                Assert.All(entry.Members, member =>
                {
                    Assert.NotEmpty(member.Name);
                    Assert.True(member.Role.IsDefined());
                });
                Assert.All(entry.Games, game =>
                {
                    Assert.True(game.Result.IsDefined());
                    Assert.True(game.Team.IsDefined());
                    Assert.True(game.RatingType.IsDefined());
                });
            });
        }
    }
}
