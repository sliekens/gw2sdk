using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Wvw.Guilds;

public class AccountWvwGuild
{
    [Fact]
    public async Task Can_be_found()
    {
        var sut = Composer.Resolve<Gw2Client>();
        ApiKey accessToken = TestConfiguration.ApiKey;

        (GuildWars2.Wvw.Guilds.AccountWvwGuild actual, _) = await sut.Wvw.GetAccountWvwGuild(
            accessToken.Key,
            TestContext.Current.CancellationToken
        );

        if (actual.TeamId.HasValue)
        {
            Assert.True(actual.TeamId > 0);
        }

        Assert.NotEmpty(actual.GuildId!);
    }
}
