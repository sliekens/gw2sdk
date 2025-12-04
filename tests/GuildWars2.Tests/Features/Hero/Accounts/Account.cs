using GuildWars2.Hero.Accounts;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Accounts;

[ServiceDataSource]
public class Account(Gw2Client sut)
{
    [Test]
    public async Task Basic_summary_with_any_access_token()
    {
        ApiKeyBasic accessToken = TestConfiguration.ApiKeyBasic;
        (AccountSummary actual, _) = await sut.Hero.Account.GetSummary(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.DisplayName).IsNotEmpty();
        await Assert.That(actual.Access).IsNotEmpty();
        foreach (Extensible<ProductName> product in actual.Access)
        {
            await Assert.That(product.IsDefined()).IsTrue();
            await Assert.That(product).IsNotEqualTo(ProductName.None);
        }
        await Assert.That(actual.Age).IsNotEqualTo(TimeSpan.Zero);
        await Assert.That(actual.LeaderOfGuildIds).IsNull();
        await Assert.That(actual.FractalLevel).IsNull();
        await Assert.That(actual.DailyAchievementPoints).IsNull();
        await Assert.That(actual.MonthlyAchievementPoints).IsNull();
        await Assert.That(actual.Wvw).IsNotNull();
        await Assert.That(actual.Wvw.Rank).IsNull();
    }

    [Test]
    public async Task Full_summary_with_high_trust_access_token()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (AccountSummary actual, _) = await sut.Hero.Account.GetSummary(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(actual.DisplayName).IsNotEmpty();
        await Assert.That(actual.Access).IsNotEmpty();
        foreach (Extensible<ProductName> product in actual.Access)
        {
            await Assert.That(product.IsDefined()).IsTrue();
            await Assert.That(product).IsNotEqualTo(ProductName.None);
        }
        await Assert.That(actual.LeaderOfGuildIds).IsNotNull();
        await Assert.That(actual.Age).IsNotEqualTo(TimeSpan.Zero);
        await Assert.That(actual.Created).IsNotEqualTo(default);
        await Assert.That(actual.FractalLevel).IsNotNull();
        await Assert.That(actual.DailyAchievementPoints).IsNotNull();
        await Assert.That(actual.MonthlyAchievementPoints).IsNotNull();
        await Assert.That(actual.Wvw).IsNotNull();
        await Assert.That(actual.Wvw.Rank).IsNotNull();
    }
}
