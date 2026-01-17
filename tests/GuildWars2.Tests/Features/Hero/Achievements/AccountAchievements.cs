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
        (IImmutableValueSet<AccountAchievement> actual, MessageContext context) = await sut.Hero.Achievements.GetAccountAchievements(accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(actual.Count);
        await Assert.That(context.ResultTotal).IsEqualTo(actual.Count);
        await Assert.That(actual).IsNotEmpty();
        foreach (AccountAchievement achievement in actual)
        {
            await Assert.That(achievement)
                .Member(a => a.Id, id => id.IsGreaterThan(0))
                .And.Member(a => a.Current, current => current.IsGreaterThanOrEqualTo(0))
                .And.Member(a => a.Max, max => max.IsGreaterThanOrEqualTo(0))
                .And.Member(a => a.Repeated, repeated => repeated.IsGreaterThanOrEqualTo(0));
#if NET
            string json = JsonSerializer.Serialize(achievement, Common.TestJsonContext.Default.AccountAchievement);
            AccountAchievement? roundTrip = JsonSerializer.Deserialize(json, Common.TestJsonContext.Default.AccountAchievement);
#else
            string json = JsonSerializer.Serialize(achievement);
            AccountAchievement? roundTrip = JsonSerializer.Deserialize<AccountAchievement>(json);
#endif
            await Assert.That(roundTrip).IsEqualTo(achievement);
        }
    }
}
