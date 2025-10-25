using GuildWars2.Guilds.Storage;
using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Guilds;

public class GuildStorage
{
    [Test]
    public async Task Can_be_found()
    {
        Gw2Client sut = Composer.Resolve<Gw2Client>();
        TestGuildLeader guildLeader = TestConfiguration.TestGuildLeader;
        (AccountSummary account, _) = await sut.Hero.Account.GetSummary(guildLeader.Token, cancellationToken: TestContext.Current!.CancellationToken);
        foreach (string guildId in account.LeaderOfGuildIds!)
        {
            (List<GuildStorageSlot> actual, _) = await sut.Guilds.GetGuildStorage(guildId, guildLeader.Token, cancellationToken: TestContext.Current!.CancellationToken);
            Assert.NotNull(actual);
        }
    }
}
