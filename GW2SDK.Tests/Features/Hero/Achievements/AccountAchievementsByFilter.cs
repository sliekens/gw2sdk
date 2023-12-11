using GuildWars2.Tests.TestInfrastructure;

namespace GuildWars2.Tests.Features.Hero.Achievements;

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

        var (actual, context) =
            await sut.Hero.Achievements.GetAccountAchievementsByIds(ids, accessToken.Key);

        Assert.Equal(ids.Count, actual.Count);
        Assert.NotNull(context.ResultContext);
        Assert.Equal(ids.Count, context.ResultContext.ResultCount);
        Assert.All(
            actual,
            entry =>
            {
                Assert.Contains(entry.Id, ids);
            }
        );
    }
}
