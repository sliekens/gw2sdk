using GuildWars2.Guilds.Logs;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Guilds;

[ServiceDataSource]
public class GuildLog(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        TestGuildLeader guildLeader = TestConfiguration.TestGuildLeader;
        (AccountSummary account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        foreach (string? guildId in account.LeaderOfGuildIds!)
        {
            (List<GuildLogEntry> actual, _) = await sut.Guilds.GetGuildLog(guildId, guildLeader.Token, cancellationToken: TestContext.Current!.Execution.CancellationToken);
            await Assert.That(actual).IsNotEmpty();
            using (Assert.Multiple())
            {
                foreach (GuildLogEntry entry in actual)
                {
                    await Assert.That(entry)
                        .Member(e => e.Id, id => id.IsGreaterThan(0))
                        .And.Member(e => e.Time, time => time.IsGreaterThan(DateTimeOffset.MinValue));
                    switch (entry)
                    {
                        case GuildBankActivity guildBankActivity:
                            await Assert.That(guildBankActivity.Operation.IsDefined()).IsTrue();
                            break;
                        case GuildUpgradeActivity guildUpgradeActivity:
                            await Assert.That(guildUpgradeActivity.Action.IsDefined()).IsTrue();
                            break;
                        case InfluenceActivity influenceActivity:
                            await Assert.That(influenceActivity.Activity.IsDefined()).IsTrue();
                            break;
                        case GuildMission guildMissionActivity:
                            await Assert.That(guildMissionActivity.State.IsDefined()).IsTrue();
                            if (guildMissionActivity.State == GuildMissionState.Start)
                            {
                                await Assert.That(guildMissionActivity.User).IsNotEmpty();
                            }
                            else
                            {
                                await Assert.That(guildMissionActivity.User).IsEmpty();
                            }

                            await Assert.That(guildMissionActivity.Influence).IsEqualTo(0);
                            break;
                        case MemberInvited memberInvited:
                            await Assert.That(memberInvited)
                                .Member(m => m.User, user => user.IsNotEmpty())
                                .And.Member(m => m.InvitedBy, invitedBy => invitedBy.IsNotEmpty());
                            break;
                        case MemberJoined memberJoined:
                            await Assert.That(memberJoined.User).IsNotEmpty();
                            break;
                        case MemberKicked memberKicked:
                            await Assert.That(memberKicked)
                                .Member(m => m.User, user => user.IsNotEmpty())
                                .And.Member(m => m.KickedBy, kickedBy => kickedBy.IsNotEmpty());
                            break;
                        case InviteDeclined inviteDeclined:
                            await Assert.That(inviteDeclined)
                                .Member(i => i.User, user => user.IsNotEmpty())
                                .And.Member(i => i.DeclinedBy, declinedBy => declinedBy.IsNotEmpty());
                            break;
                        case RankChange rankChange:
                            await Assert.That(rankChange)
                                .Member(r => r.User, user => user.IsNotEmpty())
                                .And.Member(r => r.OldRank, oldRank => oldRank.IsNotEmpty())
                                .And.Member(r => r.NewRank, newRank => newRank.IsNotEmpty())
                                .And.Member(r => r.ChangedBy, changedBy => changedBy.IsNotNull());
                            break;
                        case NewMessageOfTheDay newMessageOfTheDay:
                            await Assert.That(newMessageOfTheDay)
                                .Member(n => n.User, user => user.IsNotEmpty())
                                .And.Member(n => n.MessageOfTheDay, motd => motd.IsNotEmpty());
                            break;
                        case TreasuryDeposit treasuryDeposit:
                            await Assert.That(treasuryDeposit)
                                .Member(t => t.User, user => user.IsNotEmpty())
                                .And.Member(t => t.ItemId, itemId => itemId.IsGreaterThan(0))
                                .And.Member(t => t.Count, count => count.IsGreaterThan(0));
                            break;
                        default:
                            throw new InvalidOperationException($"Unexpected log entry type: {entry.GetType().Name}");
                    }
                }
            }
            // While we are here, check the ability to use a log ID as a skip token
            if (actual.Count > 3)
            {
                int skipToken = actual[3].Id;
                (List<GuildLogEntry> range, _) = await sut.Guilds.GetGuildLog(guildId, skipToken, guildLeader.Token, cancellationToken: TestContext.Current!.Execution.CancellationToken);
                await Assert.That(range.Count).IsGreaterThanOrEqualTo(3);
                using (Assert.Multiple())
                {
                    foreach (GuildLogEntry log in range)
                    {
                        await Assert.That(log.Id).IsGreaterThan(skipToken);
                    }
                }
            }
        }
    }
}
