using GuildWars2.Hero.Achievements;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Achievements;

[ServiceDataSource]
public class AccountAchievementsByPage(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_page()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        const int pageSize = 3;
        (IImmutableValueSet<AccountAchievement> actual, MessageContext context) = await sut.Hero.Achievements.GetAccountAchievementsByPage(0, pageSize, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.Links).IsNotNull();
        await Assert.That(context.PageSize).IsEqualTo(pageSize);
        await Assert.That(context.ResultCount).IsEqualTo(pageSize);
        await Assert.That(context.PageTotal!.Value).IsGreaterThan(0);
        await Assert.That(context.ResultTotal!.Value).IsGreaterThan(0);
        await Assert.That(actual.Count).IsEqualTo(pageSize);
        foreach (AccountAchievement item in actual)
        {
            await Assert.That(item).IsNotNull();
        }
    }
}
