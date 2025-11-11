using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Accounts;

[ServiceDataSource]
public class Luck(Gw2Client sut)
{
    [Test]
    public async Task Can_be_found()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (AccountLuck actual, _) = await sut.Hero.Account.GetLuck(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.True(actual.Luck > 0, "You have no luck today.");
        Assert.Equal(300, actual.MagicFind);
        Assert.True(actual.MagicFindWithoutLevelCap >= actual.MagicFind);
    }
}
