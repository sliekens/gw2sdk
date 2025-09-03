using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Accounts;

public class Luck
{
    [Fact]
    public async Task Can_be_found()
    {
        // https://github.com/gw2-api/issues/issues/133
        await Assert.ThrowsAsync<InvalidOperationException>(async () =>
        {
            Gw2Client sut = Composer.Resolve<Gw2Client>();
            ApiKey accessToken = TestConfiguration.ApiKey;

            (AccountLuck actual, _) = await sut.Hero.Account.GetLuck(
                accessToken.Key,
                cancellationToken: TestContext.Current.CancellationToken
            ).ConfigureAwait(true);

            Assert.True(actual.Luck > 0, "You have no luck today.");
            Assert.Equal(300, actual.MagicFind);
            Assert.True(actual.MagicFindWithoutLevelCap >= actual.MagicFind);
        });
    }
}
