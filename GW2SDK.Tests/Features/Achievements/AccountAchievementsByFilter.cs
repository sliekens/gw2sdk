using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Achievements;

public class AccountAchievementsByFilter
{
    [Fact]
    public async Task Can_be_filtered_by_id()
    {
        var sut = Composer.Resolve<Gw2Client>();
        var accessToken = Composer.Resolve<ApiKey>();

        HashSet<int> ids = new()
        {
            1,
            2,
            3
        };

        var actual = await sut.Achievements.GetAccountAchievementsByIds(ids, accessToken.Key);

        Assert.Equal(ids.Count, actual.Value.Count);
        Assert.NotNull(actual.ResultContext);
        Assert.Equal(ids.Count, actual.ResultContext.ResultCount);
        Assert.All(
            actual.Value,
            entry =>
            {
                Assert.Contains(entry.Id, ids);
            }
        );
    }
}
