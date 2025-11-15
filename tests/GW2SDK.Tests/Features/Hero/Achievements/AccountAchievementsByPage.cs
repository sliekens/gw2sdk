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
        (HashSet<AccountAchievement> actual, MessageContext context) = await sut.Hero.Achievements.GetAccountAchievementsByPage(0, pageSize, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.NotNull(context.Links);
        Assert.Equal(pageSize, context.PageSize);
        Assert.Equal(pageSize, context.ResultCount);
        Assert.True(context.PageTotal > 0);
        Assert.True(context.ResultTotal > 0);
        Assert.Equal(pageSize, actual.Count);
        Assert.All(actual, Assert.NotNull);
    }
}
