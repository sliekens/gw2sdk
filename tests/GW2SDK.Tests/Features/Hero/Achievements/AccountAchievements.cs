using System.Text.Json;

using GuildWars2.Hero.Achievements;
using GuildWars2.Tests.TestInfrastructure;


namespace GuildWars2.Tests.Features.Hero.Achievements;

public class AccountAchievements
{

    [Test]

    public async Task Can_be_listed()
    {

        Gw2Client sut = Composer.Resolve<Gw2Client>();

        ApiKey accessToken = TestConfiguration.ApiKey;

        (HashSet<AccountAchievement> actual, MessageContext context) = await sut.Hero.Achievements.GetAccountAchievements(accessToken.Key, cancellationToken: TestContext.Current!.CancellationToken);

        Assert.Equal(context.ResultCount, actual.Count);

        Assert.Equal(context.ResultTotal, actual.Count);

        Assert.NotEmpty(actual);

        Assert.All(actual, achievement =>
        {
            Assert.True(achievement.Id > 0);
            Assert.True(achievement.Current >= 0);
            Assert.True(achievement.Max >= 0);
            Assert.True(achievement.Repeated >= 0);
            string json = JsonSerializer.Serialize(achievement);
            AccountAchievement? roundTrip = JsonSerializer.Deserialize<AccountAchievement>(json);
            Assert.Equal(achievement, roundTrip);
        });
    }
}
