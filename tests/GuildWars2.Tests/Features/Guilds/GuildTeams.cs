using GuildWars2.Guilds.Teams;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

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
            (IImmutableValueList<GuildTeam> actual, _) = await sut.Guilds.GetGuildTeams(guildId, guildLeader.Token, cancellationToken: TestContext.Current!.Execution.CancellationToken);
            await Assert.That(actual).IsNotNull();
            using (Assert.Multiple())
            {
                foreach (GuildTeam entry in actual)
                {
                    await Assert.That(entry)
                        .Member(e => e.Id, m => m.IsGreaterThan(0))
                        .And.Member(e => e.State.IsDefined(), m => m.IsTrue())
                        .And.Member(e => e.Name, m => m.IsNotEmpty());
                    foreach (GuildTeamMember member in entry.Members)
                    {
                        await Assert.That(member)
                            .Member(m => m.Name, n => n.IsNotEmpty())
                            .And.Member(m => m.Role.IsDefined(), r => r.IsTrue());
                    }
                    foreach (Game game in entry.Games)
                    {
                        await Assert.That(game)
                            .Member(g => g.Result.IsDefined(), r => r.IsTrue())
                            .And.Member(g => g.Team.IsDefined(), t => t.IsTrue())
                            .And.Member(g => g.RatingType.IsDefined(), rt => rt.IsTrue());
                    }
                }
            }
        }
    }
}
