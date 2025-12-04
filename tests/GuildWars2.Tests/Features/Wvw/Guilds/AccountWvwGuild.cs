using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Wvw.Guilds;

[ServiceDataSource]
public class AccountWvwGuild(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (GuildWars2.Wvw.Guilds.AccountWvwGuild actual, _) = await sut.Wvw.GetAccountWvwGuild(accessToken.Key, TestContext.Current!.Execution.CancellationToken);
        if (actual.TeamId.HasValue)
        {
            await Assert.That(actual.TeamId > 0).IsTrue();
        }

        await Assert.That(actual.GuildId!).IsNotEmpty();
    }
}
