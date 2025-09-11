using GuildWars2.Guilds.Logs;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildLog
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
        foreach (string? guildId in account.LeaderOfGuildIds!)
        {
            (List<GuildLogEntry> actual, _) = await sut.Guilds.GetGuildLog(
                guildId,
                guildLeader.Token,
                cancellationToken: TestContext.Current.CancellationToken
            );

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
                        case GuildMission guildMissionActivity:
                            Assert.True(guildMissionActivity.State.IsDefined());
                            if (guildMissionActivity.State == GuildMissionState.Start)
                            {
                                Assert.NotEmpty(guildMissionActivity.User);
                            }
                            else
                            {
                                Assert.Empty(guildMissionActivity.User);
                            }

                            Assert.Equal(0, guildMissionActivity.Influence);
                            break;
                        case MemberInvited memberInvited:
                            Assert.NotEmpty(memberInvited.User);
                            Assert.NotEmpty(memberInvited.InvitedBy);
                            break;
                        case MemberJoined memberJoined:
                            Assert.NotEmpty(memberJoined.User);
                            break;
                        case MemberKicked memberKicked:
                            Assert.NotEmpty(memberKicked.User);
                            Assert.NotEmpty(memberKicked.KickedBy);
                            break;
                        case InviteDeclined inviteDeclined:
                            Assert.NotEmpty(inviteDeclined.User);
                            Assert.NotEmpty(inviteDeclined.DeclinedBy);
                            break;
                        case RankChange rankChange:
                            Assert.NotEmpty(rankChange.User);
                            Assert.NotEmpty(rankChange.OldRank);
                            Assert.NotEmpty(rankChange.NewRank);
                            Assert.NotNull(rankChange.ChangedBy);
                            break;
                        case NewMessageOfTheDay newMessageOfTheDay:
                            Assert.NotEmpty(newMessageOfTheDay.User);
                            Assert.NotEmpty(newMessageOfTheDay.MessageOfTheDay);
                            break;
                        case TreasuryDeposit treasuryDeposit:
                            Assert.NotEmpty(treasuryDeposit.User);
                            Assert.True(treasuryDeposit.ItemId > 0);
                            Assert.True(treasuryDeposit.Count > 0);
                            break;
                        default:
                            Assert.Fail(
                                $"Unexpected log entry type: {entry.GetType().Name}"
                            );
                            break;
                    }
                }
            );

            // While we are here, check the ability to use a log ID as a skip token
            if (actual.Count > 3)
            {
                int skipToken = actual[3].Id;
                (List<GuildLogEntry> range, _) = await sut.Guilds.GetGuildLog(
                    guildId,
                    skipToken,
                    guildLeader.Token,
                    cancellationToken: TestContext.Current.CancellationToken
                );
                Assert.True(range.Count >= 3);
                Assert.All(range, log => Assert.True(log.Id > skipToken));
            }
        }
    }
}
