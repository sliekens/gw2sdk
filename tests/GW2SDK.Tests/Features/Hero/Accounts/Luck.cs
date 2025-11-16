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
        await Assert.That(actual.Luck > 0).IsTrue().Because("You have no luck today.");
        await Assert.That(actual.MagicFind).IsEqualTo(300);
        await Assert.That(actual.MagicFindWithoutLevelCap >= actual.MagicFind).IsTrue();
    }
}
