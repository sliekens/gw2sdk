using System.Text.Json;

using GuildWars2.Hero.Achievements;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Achievements;

[ServiceDataSource]
public class AccountAchievements(Gw2Client sut)
{
    [Test]
    public async Task Can_be_listed()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        (HashSet<AccountAchievement> actual, MessageContext context) = await sut.Hero.Achievements.GetAccountAchievements(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        Assert.Equal(context.ResultCount, actual.Count);
        Assert.Equal(context.ResultTotal, actual.Count);
        Assert.NotEmpty(actual);
        Assert.All(actual, achievement =>
        {
            Assert.True(achievement.Id > 0);
            Assert.True(achievement.Current >= 0);
            Assert.True(achievement.Max >= 0);
            Assert.True(achievement.Repeated >= 0);
#if NET
            string json = JsonSerializer.Serialize(achievement, GuildWars2JsonContext.Default.AccountAchievement);
            AccountAchievement? roundTrip = JsonSerializer.Deserialize(json, GuildWars2JsonContext.Default.AccountAchievement);
#else
            string json = JsonSerializer.Serialize(achievement);
            AccountAchievement? roundTrip = JsonSerializer.Deserialize<AccountAchievement>(json);
#endif
            Assert.Equal(achievement, roundTrip);
        });
    }
}
