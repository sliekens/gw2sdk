using GuildWars2.Hero.Achievements;
using GuildWars2.Tests.TestInfrastructure.Composition;
using GuildWars2.Tests.TestInfrastructure.Configuration;

namespace GuildWars2.Tests.Features.Hero.Achievements;

[ServiceDataSource]
public class AccountAchievementsByFilter(Gw2Client sut)
{
    [Test]
    public async Task Can_be_filtered_by_id()
    {
        ApiKey accessToken = TestConfiguration.ApiKey;
        HashSet<int> ids = [1, 2, 3];
        (IImmutableValueSet<AccountAchievement> actual, MessageContext context) = await sut.Hero.Achievements.GetAccountAchievementsByIds(ids, accessToken.Key, cancellationToken: TestContext.Current!.Execution.CancellationToken);
        await Assert.That(context.ResultCount).IsEqualTo(ids.Count);
        await Assert.That(context.ResultTotal > ids.Count).IsTrue();
        await Assert.That(actual.Count).IsEqualTo(ids.Count);
        foreach (int id in ids)
        {
            await Assert.That(actual).Contains(found => found.Id == id);
        }
    }
}
