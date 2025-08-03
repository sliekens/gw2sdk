using GuildWars2.Guilds.Storage;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildStorage
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        TestGuildLeader guildLeader = TestConfiguration.TestGuildLeader;

        (AccountSummary account, _) = await sut.Hero.Account.GetSummary(
            guildLeader.Token,
            cancellationToken: TestContext.Current.CancellationToken
        );
        foreach (var guildId in account.LeaderOfGuildIds!)
        {
            (List<GuildStorageSlot> actual, _) = await sut.Guilds.GetGuildStorage(
                guildId,
                guildLeader.Token,
                cancellationToken: TestContext.Current.CancellationToken
            );

            Assert.NotNull(actual);
        }
    }
}
